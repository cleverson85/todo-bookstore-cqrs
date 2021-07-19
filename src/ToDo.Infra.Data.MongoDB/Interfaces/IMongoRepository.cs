using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Infra.Data.MongoDB.Interfaces
{
    public interface IMongoRepository
    {
        Task CreateAsync<TEntity>(TEntity entity) where TEntity : Entity;
        Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : Entity;
        Task<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : Entity;
        Task<TEntity> GetAsync<TEntity>(Guid id) where TEntity : Entity;
    }
}
