using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> Create(TEntity obj);
        Task<IList<TEntity>> GetAll();
    }
}
