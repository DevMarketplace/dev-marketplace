using System;
using System.Threading.Tasks;
using BusinessLogic.Utilities;
using DataAccess;
using DataAccess.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using UI.Controllers;
using UI.Models;

namespace UITests.Controllers
{
    [TestFixture]
    public class AccountControllerShould
    {
        private Mock<AccountController> _accountControllerPartialMock;
        private IUserManagerWrapper<ApplicationUser> _userManagerMock;
        private ISignInManagerWrapper<ApplicationUser> _signInManagerMock;
        private IEmailSender _emailSenderMock;
        private ILogger<AccountController> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = Mock.Of<IUserManagerWrapper<ApplicationUser>>();
            _signInManagerMock = Mock.Of<ISignInManagerWrapper<ApplicationUser>>();
            _emailSenderMock = Mock.Of<IEmailSender>();
            _loggerMock = Mock.Of<ILogger<AccountController>>();
            _accountControllerPartialMock = new Mock<AccountController>(_userManagerMock, _signInManagerMock, _emailSenderMock, _loggerMock) { CallBase = true };
        }

        [Test]
        public async Task DoCreateANewUserEntry()
        {
            //Arrange
            var model = new RegisterViewModel
            {
                Email = "test@test.com",
                Password = "super secret password",
                FirstName = "FirstName",
                LastName = "LastName"
            };
            ApplicationUser resultingCallAppUser = null;
            string resultingCallPassword = string.Empty;


            Mock.Get(_userManagerMock).Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.Is<string>(s => s == model.Password)))
                .Returns(Task<IdentityResult>.Factory.StartNew(() => new IdentityResult()))
                .Callback<ApplicationUser, string>((appUser, password) => 
                    {
                        resultingCallAppUser = appUser;
                        resultingCallPassword = password;
                    });

            //Act
            await _accountControllerPartialMock.Object.Register(model);

            //Assert
            Assert.AreEqual(model.Password, resultingCallPassword);
            Assert.AreEqual(model.Email, resultingCallAppUser.Email);
            Assert.AreEqual(model.FirstName, resultingCallAppUser.FirstName);
            Assert.AreEqual(model.LastName, resultingCallAppUser.LastName);
            Assert.AreEqual(model.Email, resultingCallAppUser.UserName);
        }

        [Test]
        public async Task DoNotCreateUserWhenUserCreationFails()
        {
            //Arrange
            var model = new RegisterViewModel
            {
                Email = "test@test.com",
                Password = "super secret password",
                FirstName = "FirstName",
                LastName = "LastName"
            };

            var identityError = new IdentityError {Code = "1", Description = "Test Error"};
            Mock.Get(_userManagerMock)
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.Is<string>(s => s == model.Password)))
                .Returns(Task<IdentityResult>.Factory.StartNew(() =>
                        IdentityResult.Failed(identityError)));

            //Act
            await _accountControllerPartialMock.Object.Register(model);

            //Assert
            Mock.Get(_userManagerMock).Verify(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }
    }
}