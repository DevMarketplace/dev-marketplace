using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BusinessLogic.Utilities;
using DataAccess.Abstractions;
using Microsoft.Extensions.Logging;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManager;
        private ISignInManagerWrapper<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserManagerWrapper<ApplicationUser> userManager, 
            ISignInManagerWrapper<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
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
        public async Task<IActionResult> Register(RegisterViewModel model)
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

            var configuration = new EmailSenderConfiguration
            {
                To =
                {
                    EmailAddress = model.Email,
                    Name = $"{model.FirstName} {model.LastName}"
                }
            };
            await _emailSender.SendEmailAsync(configuration);
            return View(model);
        }
    }
}
