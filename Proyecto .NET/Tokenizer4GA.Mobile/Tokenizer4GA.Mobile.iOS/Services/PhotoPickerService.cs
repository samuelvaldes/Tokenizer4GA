using Tokenizer4GA.Mobile.DependencyServices;
using Tokenizer4GA.Mobile.iOS.Services;
using Tokenizer4GA.Shared.Constants;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace Tokenizer4GA.Mobile.iOS.Services
{
    public class PhotoPickerService : IPhotoPickerService
    {
        private TaskCompletionSource<string> _taskCompletionSource;
        private UIImagePickerController _imagePicker;

        public Task<string> GetImageBase64Async()
        {
            _imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            _imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled += OnImagePickerCancelled;

            var window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(_imagePicker, true);

            _taskCompletionSource = new TaskCompletionSource<string>();
            return _taskCompletionSource.Task;
        }

        private void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            var image = args.EditedImage ?? args.OriginalImage;
            string imageAsBase64 = null;

            if (image != null)
            {
                NSData data;
                if (args.ReferenceUrl.PathExtension.ToLower().Equals(Strings.PngFileExtension))
                    data = image.AsPNG();
                else
                    data = image.AsJPEG(1);
                imageAsBase64 = data.GetBase64EncodedString(NSDataBase64EncodingOptions.None).ToString();
            }

            UnregisterEventHandlers();
            _taskCompletionSource.SetResult(imageAsBase64);
            _imagePicker.DismissModalViewController(true);
        }

        private void OnImagePickerCancelled(object sender, EventArgs args)
        {
            UnregisterEventHandlers();
            _taskCompletionSource.SetResult(null);
            _imagePicker.DismissModalViewController(true);
        }

        private void UnregisterEventHandlers()
        {
            _imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled -= OnImagePickerCancelled;
        }
    }
}
