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
        private ISignInManagerWrapper<ApplicationUser> _signInManager;
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
            var model = new RegisterViewModel();
            model.Companies = _companyManager.GetCompanies()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
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

            return RedirectToAction("RegistrationComplete", new { sequence = _protector.Protect(model.Email) });
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
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            if (signInResult.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");
                return Redirect(model.ReturnUrl);
            }

            if (signInResult.IsNotAllowed)
            {
                _logger.LogWarning(2, "The user account is not activated");
                var user = await _userManager.FindByEmailAsync(model.Email);
                await SendActivationEmail(user, model.ReturnUrl);

                return RedirectToAction(nameof(RegistrationComplete), new { sequence = _protector.Protect(model.Email) });
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
        public async Task<IActionResult> ConfirmEmail(string protectedSequence, string code, string returnUrl = null)
        {
            try
            {
                if (string.IsNullOrEmpty(protectedSequence))
                {
                    throw new ArgumentOutOfRangeException(nameof(protectedSequence));
                }

                var user = await _userManager.FindByIdAsync(_protector.Unprotect(protectedSequence));
                var identityResult = await _userManager.ConfirmEmailAsync(user, code);

                if (identityResult.Succeeded)
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
        public async Task<IActionResult> RegistrationComplete(string protectedSequence)
        {
            string email = _protector.Unprotect(protectedSequence);
            var user = await _userManager.FindByEmailAsync(email);

            if (user?.EmailConfirmed == false)
            {
                var model = new RegistrationCompleteViewModel { Email = email };
                return View(model);
            }

            return NotFound();
        }

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

            var emailBody = _viewRenderer.Render("ActivationEmailTemplate", new { AppUser = user, ActivationUrl = activationUrl });
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
                    Subject = AccountContent.ActivationEmailSubject
                },
                EmailBody = emailBody,
                EmailFormat = TextFormat.Html,
                SecureSocketOption = SecureSocketOptions.Auto,
                SmtpServer = _configuration.GetSection("EmailSettings")["SmtpServer"],
                SmtpPort = int.Parse(_configuration.GetSection("EmailSettings")["SmtpPort"]),
                Domain = _configuration.GetSection("EmailSettings")["Domain"]
            };

            await _emailSender.SendEmailAsync(configuration);
        }

    }
}
