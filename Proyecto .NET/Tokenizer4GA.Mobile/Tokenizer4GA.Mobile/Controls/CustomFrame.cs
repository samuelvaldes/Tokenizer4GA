using System;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.Controls
{
    public class CustomFrame : Frame
    {
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
                                                                                                   typeof(CornerRadius),
                                                                                                   typeof(CustomFrame));

        public CustomFrame()
        {
            base.CornerRadius = 0;
        }

        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}
