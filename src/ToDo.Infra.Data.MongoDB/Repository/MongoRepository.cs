using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Infra.Data.MongoDB.Interfaces;

namespace ToDo.Infra.Data.MongoDB.Repository
{
    public class MongoRepository : IMongoRepository
    {
        private readonly ILogger<MongoRepository> _logger;
        private readonly IMongoContext _mongoContext;

        public MongoRepository() { }

        public MongoRepository(ILogger<MongoRepository> logger, IMongoContext mongoContext) 
        {
            _logger = logger;
            _mongoContext = mongoContext;
        }

        public async Task CreateAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            var collection = _mongoContext.GetCollection<TEntity>();
            await collection.InsertOneAsync(entity);

            _logger.LogInformation($"{typeof(TEntity).Name} create in MongoDb: {entity.Id}");
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            var collection = _mongoContext.GetCollection<TEntity>();
            ReplaceOneResult updateResult = await collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);

            _logger.LogInformation($"{typeof(TEntity).Name} updated in MongoDb: {entity.Id}");

            return  updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            var collection = _mongoContext.GetCollection<TEntity>();
            DeleteResult deleteResult = await collection.DeleteOneAsync(c => c.Id == entity.Id);

            _logger.LogInformation($"{typeof(TEntity).Name} deleted in MongoDb: {entity.Id}");

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : Entity
        {
            return await _mongoContext.GetCollection<TEntity>().Find(_ => true).ToListAsync();            
        }

        public async Task<TEntity> GetAsync<TEntity>(Guid id) where TEntity : Entity
        {
            return await _mongoContext.GetCollection<TEntity>().Find(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
