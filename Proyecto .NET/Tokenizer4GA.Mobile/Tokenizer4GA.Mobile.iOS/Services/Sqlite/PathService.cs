using Tokenizer4GA.Shared;
using Tokenizer4GA.Shared.Services.Sqlite;
using Tokenizer4GA.Mobile.iOS.Services.Sqlite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace Tokenizer4GA.Mobile.iOS.Services.Sqlite
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, AppSettings.DatabaseName);
        }
    }
}