using Tokenizer4GA.Mobile.DependencyServices;
using Tokenizer4GA.Mobile.iOS.Services;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrlService))]
namespace Tokenizer4GA.Mobile.iOS.Services
{
    public class BaseUrlService : IBaseUrlService
    {
        public string Get() =>
            $"{NSBundle.MainBundle.BundlePath}/";
    }
}
