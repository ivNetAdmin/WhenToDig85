
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace WhenToDig90.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private SQLiteAsyncConnection _connection;

        public Repository()
        {
            _connection = Xamarin.Forms.DependencyService.Get<ISQLite>().GetAsyncConnection();
            Initialise();
        }

        private async void Initialise()
        {
            await _connection.CreateTableAsync<T>();  
        }

        public AsyncTableQuery<T> AsQueryable()
        {
            return _connection.Table<T>();
        }

        public async Task<List<T>> Get()
        {
            return await _connection.Table<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _connection.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = query.OrderBy<TValue>(orderBy);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> Get(int id)
        {
            return await _connection.FindAsync<T>(id).ConfigureAwait(false);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _connection.FindAsync<T>(predicate).ConfigureAwait(false);
        }

        public async Task<int> Insert(T entity)
        {
            return await _connection.InsertAsync(entity);
        }

        public async Task<int> Update(T entity)
        {
            return await _connection.UpdateAsync(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await _connection.DeleteAsync(entity);
        }
    }
}
