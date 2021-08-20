using Android.Graphics.Drawables;
using Tokenizer4GA.Mobile.Controls;
using Tokenizer4GA.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryWithoutUnderline), typeof(EntryWithoutUnderlineRenderer))]
namespace Tokenizer4GA.Mobile.Droid.Renderers
{
    [System.Obsolete]
    public class EntryWithoutUnderlineRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                using GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);
                Control.SetTextColor(Android.Graphics.Color.White);
            }
        }
    }
}
