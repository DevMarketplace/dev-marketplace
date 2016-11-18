using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLogic.Utilities;
using DataAccess.Abstractions;
using Microsoft.Extensions.Logging;
using UI.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using UI.Localization;
using UI.Utilities;
using BusinessLogic.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Controllers
{
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

        public AccountController(IUserManagerWrapper<ApplicationUser> userManager,
            ISignInManagerWrapper<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IDataProtectionProvider protectionProvider,
            IViewRenderer viewRenderer,
            IConfiguration configuration,
            ICompanyManager companyManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _viewRenderer = viewRenderer;
            _configuration = configuration;
            _protector = protectionProvider.CreateProtector(GetType().FullName);
            _companyManager = companyManager;
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
                }

                return View(model);
            }

            await SendActivationEmail(newUser, returnUrl);

            return RedirectToAction(nameof(RegistrationComplete), new { protectedSequence = _protector.Protect(model.Email) });
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            var model = new SignInViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
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

                return RedirectToAction(nameof(RegistrationComplete), new { protectedSequence = _protector.Protect(model.Email) });
            }

            if (signInResult.IsLockedOut)
            {
                _logger.LogWarning(2, "User account locked out.");
                return View("Lockout");
            }
            
            
            ModelState.AddModelError("LoginAttempt", "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(SignIn));
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> ConfirmEmail(string protectedSequence, string code, string returnUrl = null)
        {
            try
            {
                if (string.IsNullOrEmpty(protectedSequence))
                {
                    throw new ArgumentOutOfRangeException(nameof(protectedSequence));
                }

                var user = await _userManager.FindByEmailAsync(_protector.Unprotect(protectedSequence));
                var identityResult = await _userManager.ConfirmEmailAsync(user, code);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("SignIn", returnUrl);
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
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> RegistrationComplete(string protectedSequence)
        {
            try
            {
                string email = _protector.Unprotect(protectedSequence);
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
        public IActionResult ResetPassword(string protectedSequence, string code)
        {
            if (string.IsNullOrWhiteSpace(protectedSequence) || string.IsNullOrWhiteSpace(code))
            {
                return NotFound();
            }

            var model = new ResetPasswordViewModel
            {
                ProtectedId = protectedSequence,
                ResetToken = code
            };

            return View(model);
        }

        [HttpPost]
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

        [NonAction]
        private async Task SendPasswordResetEmail(ApplicationUser user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(nameof(ResetPassword), "Account", new { protectedSequence = _protector.Protect(user.Id), code = code }, protocol: HttpContext.Request.Scheme);
            var emailBody = _viewRenderer.Render("Account\\ForgottenPasswordEmailTemplate", new ActivationEmailViewModel { User = user, ActivationUrl = callbackUrl });
            await SendEmailAsync(user, AccountContent.ResetPasswordEmailSubjectText, emailBody);
        }

        [NonAction]
        private async Task SendActivationEmail(ApplicationUser user, string returnUrl = null)
        {
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string activationUrl = Url.Action(nameof(ConfirmEmail), "Account",
                new
                {
                    protectedSequence = _protector.Protect(user.Email),
                    code = confirmationToken,
                    returnUrl = returnUrl
                },
                protocol: HttpContext.Request.Scheme);

            var emailBody = _viewRenderer.Render("Account\\ActivationEmailTemplate", new ActivationEmailViewModel { User = user, ActivationUrl = activationUrl });
            await SendEmailAsync(user, AccountContent.ActivationEmailSubject, emailBody);
        }

        private async Task SendEmailAsync(ApplicationUser user, string subject, string emailBody)
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

            await _emailSender.SendEmailAsync(configuration);
        }
    }
}
