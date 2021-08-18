using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    public class CountToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                int count = System.Convert.ToInt32(value);

                return count > default(int);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
