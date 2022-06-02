using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetById<TID>(TID id);
        Task<IList<T>> GetAll();
        Task<IList<T>> Find(Expression<Func<T, bool>> expression);
        Task<T?> FindFirst(Expression<Func<T, bool>> expression);
        Task<IList<T>> FindWithIncludes(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<T?> FindFirstWithIncludes(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<T> Add(T obj);
        Task<T> Update(T obj);
        Task Delete<TID>(TID id);
    }
}
