using Tokenizer4GA.Shared.ViewModels.Token;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tokenizer4GA.Mobile.Pages.Token
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage() =>
            InitializeComponent();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = ViewModelLocator.Instance.Resolve<HomeViewModel>();
        }
    }
}
