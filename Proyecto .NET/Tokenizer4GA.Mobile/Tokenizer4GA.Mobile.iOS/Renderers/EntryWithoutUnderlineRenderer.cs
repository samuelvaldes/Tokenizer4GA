using Tokenizer4GA.Mobile.Controls;
using Tokenizer4GA.Mobile.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryWithoutUnderline), typeof(EntryWithoutUnderlineRenderer))]
namespace Tokenizer4GA.Mobile.iOS.Renderers
{
    public class EntryWithoutUnderlineRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 10;
            }
        }
    }
}
