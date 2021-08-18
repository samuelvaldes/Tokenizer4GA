using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    class NotNullObjectToBooleanConverter : IValueConverter
    {
        object originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            originalValue = value;
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            originalValue;
    }
}
