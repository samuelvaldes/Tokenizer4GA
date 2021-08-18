using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Tokenizer4GA.Shared.PlatformServices.Contracts
{
    public interface IDeviceService
    {
        Task<bool> LaunchExternalApp(Uri uri);

        string GetApplicationVersion();
        
        Task OpenBrowser(Uri uri);

        Task<bool> CheckAndRequestPermissionAsync(Permissions.BasePermission permission);
    }
}
