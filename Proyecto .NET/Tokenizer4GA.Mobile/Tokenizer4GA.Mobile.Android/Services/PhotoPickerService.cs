using Android.Content;
using Tokenizer4GA.Mobile.DependencyServices;
using Tokenizer4GA.Mobile.Droid.Services;
using Tokenizer4GA.Shared.Localization;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace Tokenizer4GA.Mobile.Droid.Services
{
    public class PhotoPickerService : IPhotoPickerService
    {
        public Task<string> GetImageBase64Async()
        {
            var intent = new Intent();
            intent.SetType($"{MainActivity.ImageType}*");
            intent.SetAction(Intent.ActionGetContent);

            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, LocalizedStrings.SelectFile),
                MainActivity.PickImageId);

            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<string>();

            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}
