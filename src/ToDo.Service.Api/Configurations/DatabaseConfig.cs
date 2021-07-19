using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Infra.Data.Contexts;

namespace ToDo.Service.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));
            services.AddDbContext<EventStoreContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
