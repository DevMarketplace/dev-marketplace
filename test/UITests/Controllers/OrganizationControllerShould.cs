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

using System;
using System.Collections.Generic;
using BusinessLogic.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using BusinessLogic.Managers;
using DataAccess;
using Microsoft.Extensions.Logging;
using UI.Controllers;
using System.Threading.Tasks;

namespace UITests.Controllers
{
    [TestFixture]
    public class OrganizationControllerShould
    {
        private OrganizationController _controller;
        private ICountryManager _countryManagerMock;
        private ICompanyManager _companyManagerMock;
        private ILoggerFactory _loggerFactory;
        private IUserManagerWrapper<ApplicationUser> _userManagerMock;
        private ApplicationUser _currentUser;

        [SetUp]
        public void SetUp()
        {
            _loggerFactory = Mock.Of<ILoggerFactory>();
            _countryManagerMock = Mock.Of<ICountryManager>();
            _userManagerMock = Mock.Of<IUserManagerWrapper<ApplicationUser>>();
            _companyManagerMock = Mock.Of<ICompanyManager>();
            _currentUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test_user@user.com",
                Email = "test_user@user.com"
            };

            Mock.Get(_countryManagerMock)
                .Setup(x => x.GetCountries())
                .Returns(new List<CountryBo> { new CountryBo
                {
                    IsoCountryCode = "US",
                    Name = "United States"
                } });

            _controller = new OrganizationController(_countryManagerMock, _companyManagerMock, _userManagerMock, _loggerFactory);

            Mock.Get(_userManagerMock)
                .Setup(x => x.GetUserAsync(_controller.User))
                .Returns(Task.FromResult(_currentUser));
        }

        [Test]
        public void DoGetTheUpdateOrganizationPage()
        {
            //Arrange
            string expectedView = "Update";
            var companyId = Guid.NewGuid();
            Mock.Get(_companyManagerMock)
                .Setup(x => x.IsUserCompanyAdmin(_currentUser.Id, companyId))
                .Returns(true);

            Mock.Get(_companyManagerMock)
                .Setup(x => x.Get(companyId))
                .Returns(new CompanyBo());

            //Act
            var result = _controller.Update(companyId);

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(expectedView, ((ViewResult)result).ViewName);
            Assert.IsNotNull(((ViewResult)result).Model);
        }
    }
}
