using Tokenizer4GA.Shared.Data.Models;
using Tokenizer4GA.Shared.Services.Sqlite;
using SQLite;
using System;
using System.Threading.Tasks;
using Tokenizer4GA.Shared.Helpers;
using Tokenizer4GA.Shared.PlatformServices.Contracts;
using Tokenizer4GA.Shared.Constants;

namespace Tokenizer4GA.Shared.Data
{
    public class ContextGA : BaseContext, IDbContext
    {
        private readonly IPlatformFunctionalityService _platformFunctionality;

        private readonly IRepository<AppLogin> _appLogin;

        public AsyncLock Mutex { get { return _mutex; } }

        public int UserId
        {
            get
            {
                var id = _platformFunctionality.GetSecureDataAsync(StorageKeys.UserId, "0").Result;
                return int.Parse(id);
            }
            set
            {
                var id = value.ToString();
                _platformFunctionality.SetSecureDataAsync(StorageKeys.UserId, id).Wait();
            }
        }

        public ContextGA(IPathService pathService,
            IPlatformFunctionalityService platformFunctionality)
        {
            _platformFunctionality = platformFunctionality;
            _databasePath = pathService.GetDatabasePath();
            _sqlCon = new SQLiteAsyncConnection(_databasePath);
            _ = CreateDatabaseAsync();

            Console.Out.WriteLine($"new SQLiteAsyncConnection.DatabasePath :: {_sqlCon.DatabasePath}");

            _appLogin = new Repository<AppLogin>(this);
        }

        public SQLiteAsyncConnection Connection => _sqlCon;

        public IRepository<AppLogin> AppLogin => _appLogin;

        public Task ClearDataAsync() => ClearDatabaseAsync();
    }
}
