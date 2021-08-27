using Tokenizer4GA.Shared.Services.Sqlite;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.PlatformServices
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            return DependencyService.Get<IPathService>().GetDatabasePath();
        }

        public string GetCertificatePath()
        {
            return DependencyService.Get<IPathService>().GetCertificatePath();
        }

        public bool ExistCertificate()
        {
            return DependencyService.Get<IPathService>().ExistCertificate();
        }
    }
}
