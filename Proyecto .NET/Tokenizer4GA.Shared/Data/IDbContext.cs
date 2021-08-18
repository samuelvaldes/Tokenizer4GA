using Tokenizer4GA.Shared.Data.Models;
using Tokenizer4GA.Shared.Helpers;
using SQLite;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.Data
{
    public interface IDbContext
    {
        AsyncLock Mutex { get; }
        int UserId { get; set; }
        SQLiteAsyncConnection Connection { get; }
        IRepository<AppLogin> AppLogin { get; }

        Task ClearDataAsync();
    }
}
