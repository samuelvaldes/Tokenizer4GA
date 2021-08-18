using Tokenizer4GA.Shared.Logic.General;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Converters
{
    public class Base64ToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
                return null;
            return ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(stringValue)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is StreamImageSource streamImageSourceValue))
                return null;
            var stream = streamImageSourceValue.Stream(CancellationToken.None).Result;
            return ConvertersLogic.ConvertStreamToBase64Async(stream).Result;
        }
    }
}
