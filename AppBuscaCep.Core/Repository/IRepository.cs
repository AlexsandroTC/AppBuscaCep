using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppBuscaCep.Core.Repository
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> GetByEntityIdAsync(Guid entityId);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
