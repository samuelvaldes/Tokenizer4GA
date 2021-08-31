using Tokenizer4GA.Shared;
using Tokenizer4GA.Shared.Services.Sqlite;
using Tokenizer4GA.Mobile.Droid.Services.Sqlite;
using System;
using System.IO;
using Xamarin.Forms;
using Tokenizer4GA.Shared.Constants;

[assembly: Dependency(typeof(PathService))]
namespace Tokenizer4GA.Mobile.Droid.Services.Sqlite
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, AppSettings.DatabaseName);
        }

        public string GetCertificatePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string pathCertificate = Path.Combine(path, AppSettings.PathComplementCertificate);

            if (!Directory.Exists(pathCertificate))
            {
                Directory.CreateDirectory(pathCertificate);
            }

            return Path.Combine(path, $"{AppSettings.CertificateName}.{Strings.XmlFileExtension}");
        }

        public bool ExistCertificate()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string pathCertificate = Path.Combine(path, AppSettings.PathComplementCertificate, $"{AppSettings.CertificateName}.{Strings.XmlFileExtension}");

            return File.Exists(pathCertificate);
        }
    }
}