using System.Net;

namespace UI.Utilities
{
    public class UrlUtilityWrapper : IUrlUtilityWrapper
    {
        public string UrlEncode(string urlToEncode)
        {
            return WebUtility.UrlEncode(urlToEncode);
        }

        public string UrlDecode(string urlToDecode)
        {
            return WebUtility.UrlDecode(urlToDecode);
        }
    }
}
