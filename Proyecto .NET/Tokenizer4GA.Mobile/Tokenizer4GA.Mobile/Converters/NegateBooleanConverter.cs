using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool boolValue))
                return null;
            return !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool boolValue))
                return null;
            return !boolValue;
        }
    }
}
