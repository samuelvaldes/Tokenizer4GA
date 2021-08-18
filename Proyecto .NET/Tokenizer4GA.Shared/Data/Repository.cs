using Tokenizer4GA.Shared.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tokenizer4GA.Shared.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        //private readonly IDbContext _context;
        private readonly AsyncLock _mutex;
        private readonly SQLiteAsyncConnection _sqlCon;

        public Repository(IDbContext context)
        {
            //_context = context;
            _mutex = context.Mutex;
            _sqlCon = context.Connection;
        }

        public AsyncTableQuery<T> AsQueryable() =>
            _sqlCon.Table<T>();

        public async Task<List<T>> Get() =>
            await _sqlCon.Table<T>().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _sqlCon.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> Get(int id) =>
             await _sqlCon.FindAsync<T>(id);

        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
            await _sqlCon.FindAsync<T>(predicate);

        public async Task<int> Insert(T entity) =>
             await _sqlCon.InsertAsync(entity);

        public async Task<int> Update(T entity) =>
             await _sqlCon.UpdateAsync(entity);

        public async Task<int> Delete(T entity) =>
             await _sqlCon.DeleteAsync(entity);


        public async Task<int> InsertItem(T item)
        {
            int result = 0;
            try
            {
                using (await _mutex.LockAsync().ConfigureAwait(false))
                {
                    /*var existingItem = await _sqlCon.Table<T>()
                            .Where(x => x.Id == item.Id)
                            .FirstOrDefaultAsync();*/

                    T existingItem = await _sqlCon.FindAsync<T>(item.Id);
                    if(item is IEntityTracking)
                    {
                        ((IEntityTracking)item).CreatedOn = existingItem != null ? ((IEntityTracking)existingItem).CreatedOn : DateTime.UtcNow;
                        ((IEntityTracking)item).UpdatedOn = DateTime.UtcNow;
                    }

                    if (existingItem == null)
                    {
                        result = await _sqlCon.InsertAsync(item).ConfigureAwait(false);
                    }
                    else
                    {
                        item.Id = existingItem.Id;
                        result = await _sqlCon.UpdateAsync(item).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task SaveItem(T item)
        {
            using (await _mutex.LockAsync().ConfigureAwait(false))
                await _sqlCon.InsertOrReplaceAsync(item);
        }
    }
}
