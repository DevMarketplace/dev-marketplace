using System;
using System.Linq;
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
        public void DoThrowAnExceptionWhenMinimumLengthCantAccomodateSelectedOptions()
        {
            //Act/Assert
            Assert.Throws<InvalidOperationException>(() => { _passwordGenerator.GeneratePassword(2, 10, true, true, true); });
        }

        [Test]
        public void DoThrowAnExceptionWhenMinimumLengthIsHigherThanMaxLength()
        {
            //Act
            var exception = Assert.Throws<AggregateException>(() => { _passwordGenerator.GeneratePassword(10, 2, true, true, true); });

            //Assert
            Assert.IsTrue(exception.InnerExceptions.Count == 2);
            Assert.IsTrue(exception.InnerExceptions.All(x => x is ArgumentOutOfRangeException));
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
            //Arrange
            bool hasUppercase = false;

            //Act
            var result = _passwordGenerator.GeneratePassword(5, 10, true, false, false);

            //Assert
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

        [Test]
        public void DoGenerateSpecialSymbols()
        {
            //Arrange
            bool hasSpecialSymbol = false;

            //Act
            var result = _passwordGenerator.GeneratePassword(5, 10, true, false, true);

            //Assert
            foreach (char letter in result)
            {
                hasSpecialSymbol = (byte)letter >= 33 && (byte)letter <= 47;
                if (hasSpecialSymbol)
                {
                    break;
                }
            }

            Assert.IsTrue(hasSpecialSymbol);
        }

        [Test]
        public void DoAddNumbersToGeneratedPassword()
        {
            //Arrange
            bool hasNumber = false;

            //Act
            var result = _passwordGenerator.GeneratePassword(5, 10, false, true, true);

            //Assert
            foreach (char letter in result)
            {
                hasNumber = (byte)letter >= 48 && (byte)letter <= 57;
                if (hasNumber)
                {
                    break;
                }
            }

            Assert.IsTrue(hasNumber);
        }

        [Test]
        public void DoAddNumbersUppercaseLowercaseAndSymbols()
        {
            //Arrange
            bool numberSet = false;
            bool symbolSet = false;
            bool uppercaseSet = false;

            //Act
            var result = _passwordGenerator.GeneratePassword(4, 15, true, true, true);

            //Assert
            foreach (char letter in result)
            {
                var hasSymbol = (byte)letter >= 33 && (byte)letter <= 47;
                if (hasSymbol)
                {
                    symbolSet = true;
                }

                var hasNumber = (byte)letter >= 48 && (byte)letter <= 57;
                if (hasNumber)
                {
                    numberSet = true;
                }

                var hasUppercase = (byte)letter >= 65 && (byte)letter <= 90;
                if (hasUppercase)
                {
                    uppercaseSet = true;
                }

                if (numberSet && symbolSet && uppercaseSet)
                {
                    break;
                }
            }

            Assert.IsTrue(numberSet);
            Assert.IsTrue(symbolSet);
            Assert.IsTrue(uppercaseSet);
        }
    }
}
