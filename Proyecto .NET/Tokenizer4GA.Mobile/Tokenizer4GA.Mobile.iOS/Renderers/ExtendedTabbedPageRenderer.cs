using Tokenizer4GA.Mobile.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace Tokenizer4GA.Mobile.iOS.Renderers
{
    public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
        [System.Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override void ViewWillAppear(bool animated)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            if (TabBar?.Items == null)
                return;

            var tabs = Element as TabbedPage;

            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                {
                    UpdateTabBarItem(TabBar.Items[i]);
                }
            }

            base.ViewWillAppear(animated);
        }

        private void UpdateTabBarItem(UITabBarItem item)
        {
            if (item == null)
                return;

            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.SystemFontOfSize(15), TextColor = Color.FromHex("#757575").ToUIColor() }, UIControlState.Normal);
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.SystemFontOfSize(15), TextColor = Color.FromHex("#3C9BDF").ToUIColor() }, UIControlState.Selected);

        }
    }
}
