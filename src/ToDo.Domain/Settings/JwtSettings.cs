using Microsoft.Extensions.Configuration;

namespace ToDo.Domain.Settings
{
    public class JwtSettings
    {
        public string SecurityKey { get; init; }
        public string ValidIssuer { get; init; }
        public string ValidAudience { get; init; }
        public string ExpiryInMinutes { get; init; }

        public JwtSettings(IConfiguration configuration)
        {
            SecurityKey = configuration["JwTSettings:SecurityKey"];
            ValidIssuer = configuration["JwTSettings:ValidIssuer"];
            ValidAudience = configuration["JwTSettings:ValidAudience"];
            ExpiryInMinutes = configuration["JwTSettings:ExpiryInMinutes"];
        }
    }
}
