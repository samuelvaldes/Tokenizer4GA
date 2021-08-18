using Tokenizer4GA.Shared.Services.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tokenizer4GA.Mobile.PlatformServices
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            return databasePath;
        }
    }
}
