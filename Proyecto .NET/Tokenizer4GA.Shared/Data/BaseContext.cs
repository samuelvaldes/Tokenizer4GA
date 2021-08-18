using Tokenizer4GA.Shared.Data.Models;
using Tokenizer4GA.Shared.Helpers;
using SQLite;
using System;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.Data
{
    public class BaseContext
    {
        protected static readonly AsyncLock _mutex = new AsyncLock();
        protected SQLiteAsyncConnection _sqlCon;
        protected string _databasePath;

        public BaseContext()
        {
            //CreateDatabaseAsync();
        }

        protected async Task CreateDatabaseAsync()
        {
            try
            {
                using (await _mutex.LockAsync().ConfigureAwait(false))
                {
                    _sqlCon.CreateTableAsync<AppLogin>().Wait();
                }
            }
            catch (Exception ex) {
                Console.Out.WriteLine(ex.Message);
            }
        }

        protected async Task ClearDatabaseAsync()
        {
             try
             {
                using (await _mutex.LockAsync().ConfigureAwait(false))
                {
                    Console.Out.WriteLine($"get SQLiteAsyncConnection.DatabasePath :: {_sqlCon.DatabasePath}");
                    await _sqlCon.ExecuteAsync("vacuum"); //.ConfigureAwait(false);
                }
             }
             catch (Exception e) {
                 Console.Out.WriteLine($"Error al limpiar tablas locales :: {e}");
             }
        }

        protected async Task DropTablesAsync()
        {
            try
            {
                using (await _mutex.LockAsync().ConfigureAwait(false))
                {
                    await _sqlCon.ExecuteAsync("vacuum");
                }
            }
            catch (Exception e) {
                Console.Out.WriteLine($"Error al limpiar tablas locales :: {e}");
            }
        }
    }
}
