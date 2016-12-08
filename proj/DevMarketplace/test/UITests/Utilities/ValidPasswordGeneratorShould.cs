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

        [Test]
        public void DoGenerateUppercaseCharacters()
        {
            //Act
            var result = _passwordGenerator.GeneratePassword(5, 10, true, false, false);

            //Assert
            bool hasUppercase = false;
            foreach (char letter in result)
            {
                hasUppercase = (byte)letter >= 65 && (byte)letter <= 90;
                if(hasUppercase)
                {
                    break;
                }
            }

            Assert.IsTrue(hasUppercase);
        }
    }
}
