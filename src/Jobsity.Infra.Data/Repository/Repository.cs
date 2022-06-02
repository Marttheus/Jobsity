using Jobsity.Domain.Interfaces;
using Jobsity.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jobsity.Infra.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<T> Add(T obj)
        {
            await _dataContext.AddAsync(obj);
            await _dataContext.SaveChangesAsync();

            return obj;
        }

        public async Task Delete(string id)
        {
            var obj = await GetById(id);
            if (obj is not null) _dataContext.Set<T>().Remove(obj);
        }

        public async Task<IList<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _dataContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IList<T>> GetAll()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(string id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> FindWithIncludes(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var query = _dataContext.Set<T>().Where(filter);

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return await query.ToListAsync();
        }

        public async Task<T> Update(T obj)
        {
            _dataContext.Set<T>().Update(obj);
            await _dataContext.SaveChangesAsync();

            return obj;
        }

        public async Task<T?> FindFirstWithIncludes(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var query = _dataContext.Set<T>().Where(filter);

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> FindFirst(Expression<Func<T, bool>> expression)
        {
            return await _dataContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }
    }
}
