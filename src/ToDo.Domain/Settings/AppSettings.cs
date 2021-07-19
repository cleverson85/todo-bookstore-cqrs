
using Microsoft.Extensions.Configuration;

namespace ToDo.Domain.Settings
{
    public class AppSettings
    {
        public string DefaultConnectionString { get; init; }
        public string RedisConnectionString { get; init; }
        public string ClientId { get; init; }
        public JwtSettings JwtSettings { get; init; }
        public RabbitMQSettings RabbitMQSettings { get; init; }
        public MongoSettings MongoSettings { get; init; }
        public MinioSettings MinioSettings { get; init; }

        public AppSettings(IConfiguration configuration)
        {
            DefaultConnectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            RedisConnectionString = configuration.GetSection("ConnectionStrings")["RedisConnection"];
            ClientId = configuration.GetSection("Authentication:Google")["ClientId"];
            JwtSettings = new JwtSettings(configuration);
            RabbitMQSettings = new RabbitMQSettings(configuration);
            MongoSettings = new MongoSettings(configuration);
            MinioSettings = new MinioSettings(configuration);
        }
    }
}