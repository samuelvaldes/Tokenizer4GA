using Android.App;
using Android.Content;
using Tokenizer4GA.Shared.Constants;
using Tokenizer4GA.Shared.ViewModels.Environment;
using Firebase.Messaging;
using System.Diagnostics;
using Xamarin.Essentials;

namespace Tokenizer4GA.Mobile.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class NotificationsService : FirebaseMessagingService
    {
        public override void OnNewToken(string fcmToken)
        {
            Debug.WriteLine($"FCM token: {fcmToken}");

            var appShellVm = ViewModelLocator.Instance?.Resolve<AppShellViewModel>();
            if (appShellVm != null)
                _ = appShellVm.ManageFirebaseCloudMessagingToken(fcmToken);
            else
                Preferences.Set(StorageKeys.FcmTokenPendingUpload, true.ToString());

#if DEBUG
            Preferences.Set(StorageKeys.FirebaseCloudMessagingToken, fcmToken);
#else
            _ = SecureStorage.SetAsync(StorageKeys.FirebaseCloudMessagingToken, fcmToken);
#endif

            base.OnNewToken(fcmToken);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            var titleVm = ViewModelLocator.Instance?.Resolve<TitleViewModel>();
            if (titleVm != null)
                titleVm.HasNotifications = true;

            base.OnMessageReceived(remoteMessage);
        }
    }
}
