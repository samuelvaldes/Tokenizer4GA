using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    public class DateShortStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime dateTimeValue))
                return null;
            return dateTimeValue.ToString(culture.DateTimeFormat.LongDatePattern, culture.DateTimeFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
                return null;
            return DateTime.Parse(stringValue, culture.DateTimeFormat);
        }
    }
}
