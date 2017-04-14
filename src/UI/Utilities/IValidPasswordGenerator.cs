namespace UI.Utilities
{
    /// <summary>
    /// A password generator that generates password strings
    /// </summary>
    public interface IValidPasswordGenerator
    {
        /// <summary>
        /// Generates a password string that is valid for the Dev Marketplace project
        /// </summary>
        /// <returns></returns>
        string GenerateValidPassword();

        /// <summary>
        /// Generates a password string based on some security criteria
        /// </summary>
        /// <param name="length"></param>
        /// <param name="addUpperCase"></param>
        /// <param name="addNumber"></param>
        /// <param name="addSymbol"></param>
        /// <returns></returns>
        string GeneratePassword(int minLength, int maxLength, bool addUpperCase, bool addNumber, bool addSymbol);
    }
}
