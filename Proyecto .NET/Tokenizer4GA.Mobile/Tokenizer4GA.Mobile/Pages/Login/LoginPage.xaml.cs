using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.ViewModels.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tokenizer4GA.Mobile.Pages.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(TargetPage), Locations.TargetPage)]
    [QueryProperty(nameof(TargetPageParameters), Locations.TargetPageParameters)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _vm;

        public LoginPage()
        {
            InitializeComponent();
            _vm = ViewModelLocator.Instance.Resolve<LoginViewModel>();
            BindingContext = _vm;
        }

        public string TargetPage
        {
            set => _vm.TargetPage = value;
        }

        public string TargetPageParameters
        {
            set => _vm.TargetPageParameters = value;
        }
    }
}