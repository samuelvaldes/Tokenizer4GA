using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using Tokenizer4GA.Shared.Constants;
using Plugin.CurrentActivity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tokenizer4GA.Mobile.Droid
{
    [Activity(Theme = "@style/LaunchTheme", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            base.OnCreate(savedInstanceState);

            IsPlayServicesAvailable();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.SetFlags("Expander_Experimental", "SwipeView_Experimental");
            
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);

            Acr.UserDialogs.UserDialogs.Init(this);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            _ = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            LoadApplication(new App());

            SetTheme(Resource.Style.MainTheme);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region PhotoPickerService
        public const string ImageType = "image/";

        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<string> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                string imageAsBase64 = null;

                if (resultCode == Result.Ok
                    && intent != null)
                {
                    var uri = intent.Data;
                    var type = ContentResolver.GetType(uri);
                    var stream = ContentResolver.OpenInputStream(uri);
                    var bitmap = BitmapFactory.DecodeStream(stream);
                    var memoryStream = new MemoryStream();
                    if (type.Split(new string[] { ImageType }, StringSplitOptions.None)[0].ToLower().Equals(Strings.PngFileExtension))
                        bitmap.Compress(Bitmap.CompressFormat.Png, 100, memoryStream);
                    else
                        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, memoryStream);
                    byte[] bytes = memoryStream.ToArray();
                    imageAsBase64 = Base64.EncodeToString(bytes, Base64Flags.Default);
                }

                PickImageTaskCompletionSource.SetResult(imageAsBase64);
            }
        }
        #endregion PhotoPickerService

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    System.Diagnostics.Debug.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    System.Diagnostics.Debug.WriteLine("This device is not supported");
                    //msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Google Play Services is available");
                return true;
            }
        }
    }
}
