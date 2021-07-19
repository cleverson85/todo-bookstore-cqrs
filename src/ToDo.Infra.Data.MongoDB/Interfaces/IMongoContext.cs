using MongoDB.Driver;
using NetDevPack.Domain;

namespace ToDo.Infra.Data.MongoDB.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : Entity;
    }
}
