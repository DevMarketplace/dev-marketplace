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
