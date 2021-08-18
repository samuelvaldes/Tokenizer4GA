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
    }
}
