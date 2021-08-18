using System;
using System.Globalization;

namespace Tokenizer4GA.Shared.Extensions
{
    public static class StringExtensions
    {
        public static int? ToInt32(this string value)
        {
            return int.TryParse(value, out int result) ? result : (int?)null;
        }

        public static DateTime? ToDateTime(this string date)
        {
            IFormatProvider provider = CultureInfo.InvariantCulture;
            return DateTime.TryParseExact(date, "dd/MM/yyyy", provider, DateTimeStyles.None, out DateTime result) ? result : (DateTime?)null;
        }

        /// <summary>
        /// Determines whether a string is a valid date value.
        /// </summary>
        /// <param name="date">The string to test.</param>
        /// <returns> for a valid date value; otherwise, <c>false</c>.</returns>
        public static bool IsDate(this string date) => DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

        /// <summary>
        /// Determines whether a string is a valid bool value.
        /// </summary>
        /// <param name="valor">The string to test.</param>
        /// <returns> for a valid bool value; otherwise, <c>false</c>.</returns>
        public static bool IsBoolean(this string valor) => bool.TryParse(valor, out _);

        /// <summary>
        /// Determines whether a string is a valid decimal number.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <returns><c>true</c> for a valid decimal number; otherwise, <c>false</c>.</returns>
        public static bool IsDecimal(this string str)
            => decimal.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out _);

        /// <summary>
        /// Determines whether a string is a valid integer.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <returns><c>true</c> for a valid integer; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(this string str) => int.TryParse(str, out _);
    }
}
