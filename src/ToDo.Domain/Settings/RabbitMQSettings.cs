using Microsoft.Extensions.Configuration;

namespace ToDo.Domain.Settings
{
    public class RabbitMQSettings
    {
        public string Url { get; init; }
        public string Usuario { get; init; }
        public string Senha { get; init; }
        public string Vhost { get; init; }
        public string Queue { get; init; }

        public RabbitMQSettings(IConfiguration configuration)
        {
            Url = configuration["RabbitMQSettings:Url"];
            Usuario = configuration["RabbitMQSettings:Usuario"];
            Senha = configuration["RabbitMQSettings:Senha"];
            Vhost = configuration["RabbitMQSettings:Vhost"];
            Queue = configuration["RabbitMQSettings:Queue"];
        }
    }
}
