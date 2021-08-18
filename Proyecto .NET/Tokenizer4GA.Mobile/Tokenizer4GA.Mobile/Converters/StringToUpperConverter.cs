using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    class StringToUpperConverter : IValueConverter
    {
        object originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            originalValue = value;
            if (!(value is string stringValue))
                return null;
            return stringValue.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            originalValue;
    }
}
