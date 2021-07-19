using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using NetDevPack.Domain;
using ToDo.Domain.Settings;
using ToDo.Infra.Data.MongoDB.Interfaces;

namespace ToDo.Infra.Data.MongoDB.Context
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(AppSettings appSettings)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            var client = new MongoClient(appSettings.MongoSettings.MongoConnectionString);
            _database = client.GetDatabase(appSettings.MongoSettings.DataBase);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : Entity
        {
            return _database.GetCollection<TEntity>($"{typeof(TEntity).Name}-Collection");
        }
    }
}
