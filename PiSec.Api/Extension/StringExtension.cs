using System.Text.RegularExpressions;

namespace PiSec.Api.Extension
{
    public static class StringExtension
    {
        private static readonly Regex EmailRegex = new Regex(
        @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?" +
        @"(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }
    }
}
