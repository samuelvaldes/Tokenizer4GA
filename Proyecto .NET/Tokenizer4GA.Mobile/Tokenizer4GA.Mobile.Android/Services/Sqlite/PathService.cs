using Tokenizer4GA.Shared;
using Tokenizer4GA.Shared.Services.Sqlite;
using Tokenizer4GA.Mobile.Droid.Services.Sqlite;
using System;
using System.IO;
using Xamarin.Forms;

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
    }
}