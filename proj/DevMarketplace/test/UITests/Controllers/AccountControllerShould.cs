using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Facade;
using DataAccess;
using Microsoft.AspNetCore.Identity;
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
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private Mock<IEmailSender> _emailSenderMock;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = new Mock<UserManager<ApplicationUser>>();
            _signInManagerMock  = new Mock<SignInManager<ApplicationUser>>();
            _emailSenderMock = new Mock<IEmailSender>();
            _accountControllerPartialMock = new Mock<AccountController>(_userManagerMock.Object, _signInManagerMock.Object, _emailSenderMock.Object) { CallBase = true };
        }

        [Test]
        public void DoCreateANewUserEntry()
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

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.Is<string>(s => s == model.Password)))
                .Returns(Task<IdentityResult>.Factory.StartNew(() => new IdentityResult()))
                .Callback<ApplicationUser, string>((appUser, password) => 
                    {
                        resultingCallAppUser = appUser;
                        resultingCallPassword = password;
                    });

            //Act
            _accountControllerPartialMock.Object.Register(model).Wait();

            //Assert
            Assert.AreEqual(model.Password, resultingCallPassword);
            Assert.AreEqual(model.Email, resultingCallAppUser.Email);
            Assert.AreEqual(model.FirstName, resultingCallAppUser.FirstName);
            Assert.AreEqual(model.LastName, resultingCallAppUser.LastName);
            Assert.AreEqual(model.Email, resultingCallAppUser.UserName);
        }
    }
}
