#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
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

using System.Threading.Tasks;
using BusinessLogic.Managers;
using BusinessLogic.Utilities;
using DataAccess;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using UI.Controllers;
using UI.Models;
using UI.Utilities;

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
        private IViewRenderer _viewRendererMock;
        private IConfiguration _configurationMock;
        private IDataProtector _protectorMock;
        private ICompanyManager _companyManagerMock;
        private IUrlUtilityWrapper _urlEncoderWrapper;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = Mock.Of<IUserManagerWrapper<ApplicationUser>>();
            _signInManagerMock = Mock.Of<ISignInManagerWrapper<ApplicationUser>>();
            _emailSenderMock = Mock.Of<IEmailSender>();
            _loggerMock = Mock.Of<ILogger<AccountController>>();
            _viewRendererMock = Mock.Of<IViewRenderer>();
            _protectorMock = Mock.Of<IDataProtector>();
            _configurationMock = Mock.Of<IConfiguration>();
            _companyManagerMock = Mock.Of<ICompanyManager>();
            _urlEncoderWrapper = new UrlUtilityWrapper();
            _accountControllerPartialMock = new Mock<AccountController>(_userManagerMock, 
                _signInManagerMock, _emailSenderMock, _loggerMock, 
                _protectorMock, _viewRendererMock, 
                _configurationMock, _companyManagerMock, _urlEncoderWrapper) { CallBase = true };
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