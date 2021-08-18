using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    class NegateNullableBooleanConverter : IValueConverter
    {
        object originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            originalValue = value;
            var nullableBoolValue = value as bool?;
            if (!nullableBoolValue.HasValue)
                return false;
            return !nullableBoolValue.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            originalValue;
    }
}
