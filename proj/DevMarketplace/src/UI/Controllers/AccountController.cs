using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLogic.Utilities;
using DataAccess.Abstractions;
using Microsoft.Extensions.Logging;
using UI.Models;
using Microsoft.AspNetCore.DataProtection;
using System;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManager;
        private ISignInManagerWrapper<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        private readonly IDataProtector _protector;

        public AccountController(IUserManagerWrapper<ApplicationUser> userManager, 
            ISignInManagerWrapper<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IDataProtectionProvider protectionProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _protector = protectionProvider.CreateProtector(GetType().FullName);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            var newUser = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Registration error: {error.Code} - {error.Description}");
                }

                return View(model);
            }

            await SendActivationEmail(newUser);
            
            return RedirectToAction("RegistrationComplete", new { sequence = _protector.Protect(model.Email) });
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            var model = new SignInViewModel();
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            
            if (signInResult.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return Redirect(model.ReturnUrl);
            }

            if(signInResult.IsNotAllowed)
            {
                _logger.LogWarning(2, "The user account is not activated");
                var user = await _userManager.FindByEmailAsync(model.Email);
                await SendActivationEmail(user);

                return RedirectToAction(nameof(RegistrationComplete), new { sequence =  _protector.Protect(model.Email)});
            }

            if (signInResult.IsLockedOut)
            {
                _logger.LogWarning(2, "User account locked out.");
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> ConfirmEmail(string sequence, string code, string returnUrl = null)
        {
            try
            {
                if(string.IsNullOrEmpty(sequence))
                {
                    throw new ArgumentOutOfRangeException("invalid UserId");                        
                }

                var user = await _userManager.FindByIdAsync(_protector.Unprotect(sequence));
                var identityResult = await _userManager.ConfirmEmailAsync(user, code);
                
                if(identityResult.Succeeded)
                {
                    RedirectToAction("SignIn", returnUrl);
                }
                else
                {
                    throw new Exception();
                }
                
                return View();
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message);
                return NotFound();
            }
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> RegistrationComplete(string sequence)
        {
            string email = _protector.Unprotect(sequence);
            var user = await _userManager.FindByEmailAsync(email);

            if(user?.EmailConfirmed == false)
            {
                var model = new RegistrationCompleteViewModel();
                model.Email = email;
                return View(model);
            }

            return NotFound();
        }

        private async Task SendActivationEmail(ApplicationUser user)
        {
            string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var configuration = new EmailSenderConfiguration
            {
                To =
                {
                    EmailAddress = user.Email,
                    Name = $"{user.FirstName} {user.LastName}"
                }
            };
            await _emailSender.SendEmailAsync(configuration);
        }
        
    }
}
