using Tokenizer4GA.Mobile.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppDelegate))]
namespace Tokenizer4GA.Mobile.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.SetFlags("Expander_Experimental", "SwipeView_Experimental");
            
            Forms.Init();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            _ = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);
            Xamarin.FormsMaps.Init();

            LoadApplication(new App());

            UINavigationBar.Appearance.Translucent = false;

            return base.FinishedLaunching(app, options);
        }
    }
}
