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
using NUnit.Framework;
using System.Net;
using UI.Utilities;

namespace UITests.Utilities
{
    [TestFixture]
    public class UrlUtilityWrapperShould
    {
        private UrlUtilityWrapper _urlUtilityWrapper;

        [SetUp]
        public void SetUp()
        {
            _urlUtilityWrapper = new UrlUtilityWrapper();
        }

        [Test]
        public void DoEncodeUrl()
        {
            //Arrange
            string urlToEncode = "http://www.test.com/333 222";
            string expectedEncodedUrl = WebUtility.UrlEncode(urlToEncode);

            //Act
            string result = _urlUtilityWrapper.UrlEncode(urlToEncode);

            //Assert
            Assert.AreEqual(expectedEncodedUrl, result);
        }

        [Test]
        public void DoDecodeUrl()
        {
            //Arrange
            string urlToDecode = "http://www.test.com/333%20222";
            string expectedResult = "http://www.test.com/333 222";

            //Act
            string result = _urlUtilityWrapper.UrlDecode(urlToDecode);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
