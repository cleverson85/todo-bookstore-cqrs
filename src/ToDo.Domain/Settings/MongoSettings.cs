using Microsoft.Extensions.Configuration;

namespace ToDo.Domain.Settings
{
    public class MongoSettings
    {
        public string MongoConnectionString { get; init; }
        public string DataBase { get; init; }

        public MongoSettings(IConfiguration configuration)
        {
            MongoConnectionString = configuration.GetSection("ConnectionStrings")["MongoConnection"];
            DataBase = configuration["MongoDataBase"];
        }
    }
}
