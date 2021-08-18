using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    class CurrentPageConverter : IValueConverter
    {
        object originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            originalValue = value;
            if (!(value is int intValue)
                || !(parameter is string stringParameter)
                || !int.TryParse(stringParameter, out var intParameter))
                return null;
            return intValue == intParameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            originalValue;
    }
}
