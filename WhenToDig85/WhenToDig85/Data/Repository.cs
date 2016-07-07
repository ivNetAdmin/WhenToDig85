
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite.Net.Async;
using WhenToDig85.Data;

namespace WhenToDig85.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private static readonly AsyncLock Mutex = new AsyncLock();
        private readonly SQLiteAsyncConnection _connection;

        public Repository()
        {
            _connection = Xamarin.Forms.DependencyService.Get<ISQLite>().GetAsyncConnection();
            Initialise();
        }

        private async void Initialise()
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                //await _connection.DeleteAllAsync<T>().ConfigureAwait(false);
                await _connection.CreateTableAsync<T>().ConfigureAwait(false);
            } 
        }

        public AsyncTableQuery<T> AsQueryable()
        {
            return _connection.Table<T>();
        }

        public async Task<List<T>> Get()
        {
            List<T> entityList = new List<T>();
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                entityList = await _connection.Table<T>().ToListAsync().ConfigureAwait(false);
            }
            return entityList;           
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
            T task;
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                task = await _connection.FindAsync<T>(predicate).ConfigureAwait(false);
            }
            return task;
        }

        public async Task<int> Insert(T entity)
        {
            int entityId = 0;
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                entityId =  await _connection.InsertAsync(entity).ConfigureAwait(false);
            }
            return entityId;
        }

        public async Task<int> Update(T entity)
        {
            int entityId = 0;
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                entityId = await _connection.UpdateAsync(entity).ConfigureAwait(false);
            }
            return entityId;
           // return await _connection.UpdateAsync(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await _connection.DeleteAsync(entity);
        }

        //public async Task Save(Conference conference)
        //{
        //    using (await Mutex.LockAsync().ConfigureAwait(false))
        //    {
        //        // Because our conference model is being mapped from the dto,
        //        // we need to check the database by name, not id
        //        var existingConference = await _connection.Table<Conference>()
        //                .Where(x => x.Slug == conference.Slug)
        //                .FirstOrDefaultAsync();

        //        if (existingConference == null)
        //        {
        //            await _connection.InsertAsync(conference).ConfigureAwait(false);
        //        }
        //        else
        //        {
        //            conference.Id = existingConference.Id;
        //            await _connection.UpdateAsync(conference).ConfigureAwait(false);
        //        }
        //    }
        //}
    }
}
