using System;

namespace UI.Utilities
{
    public class ValidPasswordGenerator : IValidPasswordGenerator
    {
        public string GenerateValidPassword()
        {
            return GeneratePassword(8, 20, true, true, false);
        }

        public string GeneratePassword(int minLength, int maxLength, bool addUpperCase, bool addNumber, bool addSymbol)
        {
            int reserved = 1;
            reserved += addUpperCase ? 1 : 0;
            reserved += addNumber ? 1 : 0;
            reserved += addSymbol ? 1 : 0;

            if (minLength < reserved)
            {
                throw new InvalidOperationException("The minimum length of the password can not accomodate all selected options.");
            }

            if (maxLength > 1500 || minLength > maxLength)
            {
                throw new AggregateException(new ArgumentOutOfRangeException(nameof(minLength), minLength, "The lengths of the password are invalid."), 
                    new ArgumentOutOfRangeException(nameof(maxLength), maxLength, "The lengths of the password are invalid."));
            }
            var generator = new Random();
            int length = generator.Next(minLength, maxLength);
            int freeLength = length - reserved;
            int arrIdx = 0;
            var passwordArray = new char[length];

            if (addNumber)
            {
                freeLength = GeneratePasswordCharacters(passwordArray, freeLength, ref arrIdx, () => char.Parse(generator.Next(0,9).ToString())); //0-9
                reserved--;
            }

            if (addUpperCase)
            {

                freeLength = GeneratePasswordCharacters(passwordArray, freeLength, ref arrIdx, () => Convert.ToChar(generator.Next(65, 90))); //A-Z
                reserved--;
            }

            if (addSymbol)
            {
                freeLength = GeneratePasswordCharacters(passwordArray, freeLength, ref arrIdx, () => Convert.ToChar(generator.Next(33, 47))); //Special symbols
                reserved--;
            }

            GeneratePasswordCharacters(passwordArray, freeLength + reserved, ref arrIdx, () => Convert.ToChar(generator.Next(97, 122)), false); //a-z

            return string.Concat(passwordArray);
        }

        private static int GeneratePasswordCharacters(char[] passwordArray, int allowedLength, ref int startPosition, Func<char> func, bool randomCount = true)
        {
            var generator = new Random();
            int numberCount = randomCount ? generator.Next(1, allowedLength) : allowedLength;
            int nextIndex = startPosition;
            for (int i = startPosition; i < numberCount; i++)
            {
                passwordArray[i] = func.Invoke();
                nextIndex++;
            }

            passwordArray[startPosition] = func.Invoke();
            nextIndex++;
            allowedLength = allowedLength - nextIndex + 1;
            startPosition = nextIndex;
            return allowedLength;
        }
    }
}
