using System.Linq;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.ExtensionMethods
{
    public static class CurrencyExtensionMethod
    {
        public static int DecimalDigits(this decimal n)
        {
            return n.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    .SkipWhile(c => c != '.')
                    .Skip(1)
                    .Count();
        }

        public static bool IsDeletion(this TextChangedEventArgs e)
        {
            return !string.IsNullOrEmpty(e.OldTextValue) && e.OldTextValue.Length > e.NewTextValue.Length;
        }
    }
}
