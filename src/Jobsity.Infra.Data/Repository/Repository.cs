using Jobsity.Domain.Interfaces;
using Jobsity.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Jobsity.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TEntity> Create(TEntity obj)
        {
            await _dataContext.AddAsync(obj);
            await _dataContext.SaveChangesAsync();

            return obj;
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await _dataContext.Set<TEntity>().ToListAsync();
        }
    }
}
