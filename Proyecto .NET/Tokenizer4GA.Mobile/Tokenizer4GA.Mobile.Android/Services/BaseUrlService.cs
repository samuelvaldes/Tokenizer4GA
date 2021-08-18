using Tokenizer4GA.Mobile.DependencyServices;
using Tokenizer4GA.Mobile.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrlService))]
namespace Tokenizer4GA.Mobile.Droid.Services
{
    public class BaseUrlService : IBaseUrlService
    {
        private const string _androidAsset = "file:///android_asset/";

        public string Get() =>
            _androidAsset;
    }
}
