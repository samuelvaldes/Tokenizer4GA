using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    class SelectFirstConverter : IValueConverter
    {
        object originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            originalValue = value;
            if (!(value is IEnumerable<object> enumerableValue))
                return null;
            return enumerableValue.FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            originalValue;
    }
}
