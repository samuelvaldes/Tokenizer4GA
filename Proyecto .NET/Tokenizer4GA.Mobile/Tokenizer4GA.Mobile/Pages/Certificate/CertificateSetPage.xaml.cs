using Tokenizer4GA.Shared.ViewModels.Certificate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tokenizer4GA.Mobile.Pages.Certificate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CertificateSetPage : ContentPage
    {
        public CertificateSetPage()
        {
            InitializeComponent();
            var vm = ViewModelLocator.Instance.Resolve<CertificateSetViewModel>();
            BindingContext = vm;
        }
    }
}
