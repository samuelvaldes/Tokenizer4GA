using Tokenizer4GA.Mobile.Pages.Token;
using Tokenizer4GA.Shared.Localization;
using Tokenizer4GA.Shared.ViewModels.General;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tokenizer4GA.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewerPage : ContentPage
    {
        
        private readonly WebViewerViewModel _vm;
        
        public bool WebViewCanGoBack
        {
            get => webView.CanGoBack;
        }

        public WebViewerPage(string title, string uri)
        {
            InitializeComponent();
            Title = title;
            webView.Source = uri;
            _vm = ViewModelLocator.Instance.Resolve<WebViewerViewModel>();
            BindingContext = _vm;
        }

        public void WebViewGoBack() =>
            webView.GoBack();

        //ToDo: Create structure to implement a ViewModel and track stack navigation
        // This functionallity gonna be implemented in the next pull request 
        async void ToolbarItem_Clicked(object sender, System.EventArgs e) =>
            await Navigation.PushAsync(new HomePage());
    }
}
