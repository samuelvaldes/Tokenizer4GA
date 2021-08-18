using Tokenizer4GA.Shared.PlatformServices.Contracts;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Tokenizer4GA.Mobile.PlatformServices
{
    public class DeviceServices : IDeviceService
    {
        public string GetApplicationVersion() => VersionTracking.CurrentVersion;

        public async Task<bool> LaunchExternalApp(Uri uri)
        {
            var support = await Launcher.CanOpenAsync(uri);
            if(support)
                await Launcher.OpenAsync(uri);
            return support;
        }

        public async Task OpenBrowser(Uri uri)
        {
            try
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {
                //ignored
            }
        }

        public static async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
        
        public async Task<bool> CheckAndRequestPermissionAsync(Permissions.BasePermission permission)
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status == PermissionStatus.Granted;
        }
    }
}
