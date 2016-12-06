using NUnit.Framework;
using UI.Utilities;

namespace UITests.Utilities
{
    [TestFixture]
    public class ValidPasswordGeneratorShould
    {
        private ValidPasswordGenerator _passwordGenerator;

        [SetUp]
        public void SetUp()
        {
            _passwordGenerator = new ValidPasswordGenerator();
        }

        [Test]
        public void DoNotGenerateUppercaseCharacters()
        {
            //Act
            var result = _passwordGenerator.GeneratePassword(5, 10, false, false, false);

            //Assert
            foreach (char letter in result)
            {
                Assert.IsTrue((byte)letter >= 97 && (byte)letter <= 122);
            }
        }
    }
}
