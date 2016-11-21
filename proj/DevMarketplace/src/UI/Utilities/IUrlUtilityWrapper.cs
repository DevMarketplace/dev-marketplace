namespace UI.Utilities
{
    public interface IUrlUtilityWrapper
    {
        /// <summary>
        /// Encodes an URL
        /// </summary>
        /// <param name="urlToEncode"></param>
        /// <returns></returns>
        string UrlEncode(string urlToEncode);

        /// <summary>
        /// Decodes an encoded URL
        /// </summary>
        /// <param name="urlToDecode"></param>
        /// <returns></returns>
        string UrlDecode(string urlToDecode);
    }
}
