using NetDevPack.Data;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Domain.Interfaces.Base
{
    public interface IBaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        Task<TEntity> SaveAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task<int> CountAsync();
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
