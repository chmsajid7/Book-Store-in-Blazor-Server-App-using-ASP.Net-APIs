using System.Linq;

namespace BlazorServerApp.Helpers
{
    public static class StringExtensions
    {
        public static string FormatNumberStringWithDigits(this string text, int digits)
        {
            var result = string.Empty;
            if (text.Length > digits)
            {
                result = text.Replace(text.Substring(0, text.Length - digits), "...");
            }
            else
            {
                result = text;
            }
            return result;
        }
    }
}
