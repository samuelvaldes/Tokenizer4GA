using Tokenizer4GA.Shared.Constants;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tokenizer4GA.Shared.Logic.General
{
    public static class ValidatorsLogic
    {
        public static bool ValidateNumericCharacters(string numericText) =>
            numericText.All(char.IsNumber);

        public static bool ValidateEmail(string emailText) =>
            Regex.IsMatch(emailText, Strings.EmailPattern, RegexOptions.IgnoreCase);

        public static bool ValidateExtendedAsciiCharacters(string text, bool acceptLineSeparators = false)
        {
            if (acceptLineSeparators)
                text = text.Replace(Environment.NewLine, string.Empty);
            return text.All(c => Regex.IsMatch(c.ToString(), Strings.ExtendedAsciiPattern));
        }

        public static bool ValidatePasswordLogin(string text)
        {
            return Regex.IsMatch(text, Strings.PasswordPattern) 
                   || Regex.IsMatch(text, Strings.GuestPasswordPattern);
        }
    }
}