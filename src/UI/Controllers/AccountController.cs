#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2017 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLogic.Utilities;
using Microsoft.Extensions.Logging;
using UI.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using UI.Localization;
using UI.Utilities;
using System.Linq;
using System.Security.Claims;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hangfire;

namespace UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManager;
        private readonly ISignInManagerWrapper<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        private readonly IViewRenderer _viewRenderer;
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _protector;
        private readonly ICompanyManager _companyManager;
        private readonly IUrlUtilityWrapper _urlEncoderWrapper;

        private readonly IBackgroundJobClient _backgroundJobClient;

        public AccountController(IUserManagerWrapper<ApplicationUser> userManager,
            ISignInManagerWrapper<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IDataProtectionProvider protectionProvider,
            IViewRenderer viewRenderer,
            IConfiguration configuration,
            ICompanyManager companyManager,
            IUrlUtilityWrapper urlEncoderWrapper,
            IBackgroundJobClient backgroundJobClient
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _viewRenderer = viewRenderer;
            _configuration = configuration;
            _protector = protectionProvider.CreateProtector(GetType().FullName);
            _companyManager = companyManager;
            _urlEncoderWrapper = urlEncoderWrapper;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                Companies = _companyManager.GetCompanies()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                model.Companies = _companyManager.GetCompanies().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
                return View(model);
            }

            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyId = model.CompanyId
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Registration error: {error.Code} - {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                model.Companies = _companyManager.GetCompanies().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

                return View(model);
            }

            try
            {
                await SendActivationEmail(newUser, returnUrl);
            } catch (Exception exp)
            {
                _logger.LogError(2, exp, "SMTP sender failed.");
            }

            return RedirectToAction(nameof(RegistrationComplete), new
            {
                protectedSequence = _urlEncoderWrapper.UrlEncode(_protector.Protect(model.Email))
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl = null)
        {
            var model = new SignInViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                ModelState.AddModelError(string.Empty, AccountContent.InvalidUsernameErrorText);
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, AccountContent.EmailConfirmationRequiredErrorText);
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (signInResult.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return Redirect(string.IsNullOrWhiteSpace(model.ReturnUrl) ? "/" : model.ReturnUrl);
            }

            if (signInResult.IsNotAllowed)
            {
                _logger.LogWarning(2, "The user account is not activated");
                await SendActivationEmail(user, model.ReturnUrl);

                return RedirectToAction(nameof(RegistrationComplete), new
                {
                    protectedSequence = _protector.Protect(_urlEncoderWrapper.UrlEncode(model.Email))
                });
            }

            if (signInResult.IsLockedOut)
            {
                _logger.LogWarning(2, "User account locked out.");
                return View("Lockout");
            }
            
            ModelState.AddModelError(string.Empty, AccountContent.InvalidLoginAttemptText);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterExternal(RegisterViewModel model, [FromQuery] string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                model.Companies = _companyManager.GetCompanies().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
                return View(model);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return View("ExternalLoginFailure");
            }

            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyId = model.CompanyId,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(newUser, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);

                    // Update any authentication tokens as well
                    await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                    
                    _companyManager.SetAdmin(Guid.Parse(newUser.Id), newUser.CompanyId);

                    if(!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
            }

            foreach (var error in result.Errors)
            {
                _logger.LogError($"Registration error: {error.Code} - {error.Description}");
                ModelState.AddModelError(string.Empty, error.Description);
            }

            model.Companies = await Task.FromResult(_companyManager.GetCompanies().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList());

            return View(nameof(SignInExternal), model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignInExternal(SignInExternalViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Provider))
            {
                return BadRequest();
            }

            var scheme = _signInManager.GetExternalAuthenticationSchemes().FirstOrDefault(x => x.DisplayName.ToLower() == model.Provider.ToLower());
            if (scheme == null)
            {
                return BadRequest();
            }

            var redirectUrl = Url.Action(nameof(SignInExternal), "Account", new { returnUrl = model.ReturnUrl, provider = model.Provider });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(model.Provider, redirectUrl);
            return Challenge(properties, scheme.DisplayName);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SignInExternal(string returnUrl, string provider, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(SignIn), new SignInViewModel());
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(SignIn));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                // Update any authentication tokens if login succeeded
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);

                if(string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return Redirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return View("Lockout");
            }

            return View(new RegisterViewModel
            {
                Provider = provider,
                FirstName = info.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                LastName = info.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                Email = info.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                Companies = _companyManager.GetCompanies()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            });
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ProfileViewModel(user, _companyManager);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Profile(ProfileViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(new ProfileViewModel(model, _companyManager));
            }

            var user = await _userManager.GetUserAsync(User);
            ProfileViewModel.SetUserProperties(model, user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error while updating the user: {error.Code} - {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(new ProfileViewModel(model, _companyManager));
            }

            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(SignIn));
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> ConfirmEmail(string protectedSequence, string code, string returnUrl = null)
        {
            try
            {
                if (string.IsNullOrEmpty(protectedSequence))
                {
                    throw new ArgumentOutOfRangeException(nameof(protectedSequence));
                }

                protectedSequence = _urlEncoderWrapper.UrlDecode(protectedSequence);
                var user = await _userManager.FindByEmailAsync(_protector.Unprotect(protectedSequence));
                var identityResult = await _userManager.ConfirmEmailAsync(user, code);

                if (identityResult.Succeeded)
                {
                    _companyManager.SetAdmin(Guid.Parse(user.Id), user.CompanyId);
                    return RedirectToAction(nameof(SignIn), returnUrl);
                }

                return NotFound();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> RegistrationComplete(string protectedSequence)
        {
            try
            {
                string email = _protector.Unprotect(_urlEncoderWrapper.UrlDecode(protectedSequence));
                var user = await _userManager.FindByEmailAsync(email);

                if (user?.EmailConfirmed == false)
                {
                    var model = new RegistrationCompleteViewModel { Email = email, FirstName = user.FirstName };
                    return View(model);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(2, e, e.Message);
                return NotFound();
            }

            return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    _logger.LogWarning($"An attempt to reset the password of an invalid e-mail was made for: {model.Email}");
                    return View("ForgotPasswordConfirmation", model);
                }

                await SendPasswordResetEmail(user);
                return View("ForgotPasswordConfirmation", model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string protectedSequence, string code)
        {
            if (string.IsNullOrWhiteSpace(protectedSequence) || string.IsNullOrWhiteSpace(code))
            {
                return NotFound();
            }

            protectedSequence = _urlEncoderWrapper.UrlDecode(protectedSequence);
            var model = new ResetPasswordViewModel
            {
                ProtectedId = protectedSequence,
                ResetToken = code
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(_protector.Unprotect(model.ProtectedId));
            var result = await _userManager.ResetPasswordAsync(user, model.ResetToken, model.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(SignIn));
            }

            foreach (var resultError in result.Errors)
            {
                _logger.LogWarning($"{resultError.Code}-{resultError.Description}");
            }

            return NotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrentUser()
        {
            var model = new AccountInfoViewModel();
            if(!User.Identity.IsAuthenticated)
            {
                return Json(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return Json(model);
            }
            
            model.FirstName = user.FirstName;
            model.Email = user.Email;
            model.Authenticated = true;

            return Json(model);
        }

        [NonAction]
        private async Task SendPasswordResetEmail(ApplicationUser user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new
            {
                protectedSequence = _urlEncoderWrapper.UrlEncode(_protector.Protect(user.Id)), code = code
            }, protocol: HttpContext.Request.Scheme);
            var emailBody = _viewRenderer.Render("Account\\ForgottenPasswordEmailTemplate", new ActivationEmailViewModel { User = user, ActivationUrl = callbackUrl });
            _backgroundJobClient.Enqueue(() => SendEmail(user, AccountContent.ResetPasswordEmailSubjectText, emailBody));
        }

        [NonAction]
        private async Task SendActivationEmail(ApplicationUser user, string returnUrl = null)
        {
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string activationUrl = Url.Action(nameof(ConfirmEmail), "Account",
                new
                {
                    protectedSequence = _urlEncoderWrapper.UrlEncode(_protector.Protect(user.Email)),
                    code = confirmationToken,
                    returnUrl = returnUrl
                },
                protocol: HttpContext.Request.Scheme);

            var emailBody = _viewRenderer.Render("Account\\ActivationEmailTemplate", new ActivationEmailViewModel { User = user, ActivationUrl = activationUrl });
            _backgroundJobClient.Enqueue(() => SendEmail(user, AccountContent.ActivationEmailSubject, emailBody));
        }

        [AutomaticRetry(Attempts = 3)]
        public void SendEmail(ApplicationUser user, string subject, string emailBody)
        {
            var configuration = new EmailSenderConfiguration
            {
                From =
                {
                    Name = AccountContent.DevMarketplaceTeamText,
                    EmailAddress = _configuration.GetSection("EmailSettings")["FromEmail"]
                },
                To =
                {
                    EmailAddress = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Subject = subject
                },
                EmailBody = emailBody,
                EmailFormat = TextFormat.Html,
                SecureSocketOption =
                    bool.Parse(_configuration.GetSection("EmailSettings")["UseSSL"])
                        ? SecureSocketOptions.Auto
                        : SecureSocketOptions.None,
                SmtpServer = _configuration.GetSection("EmailSettings")["SmtpServer"],
                SmtpPort = int.Parse(_configuration.GetSection("EmailSettings")["SmtpPort"]),
                Domain = _configuration.GetSection("EmailSettings")["Domain"]
            };

            _emailSender.SendEmailAsync(configuration).Wait();            
        }
    }
}
