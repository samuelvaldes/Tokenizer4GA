using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    public class DocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var documentDescription = string.Empty;
            if (value is string)
            {
                documentDescription = value.ToString().ToUpperInvariant();
                if (documentDescription == "TRUE")
                {
                    return "Sí";
                }
                else if (documentDescription == "FALSE")
                {
                    return "No";
                }
            }
            return documentDescription;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
