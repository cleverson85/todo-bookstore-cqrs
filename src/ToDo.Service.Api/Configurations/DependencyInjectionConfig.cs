using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Settings;
using ToDo.Infra.CrossCutting.IoC;

namespace ToDo.Service.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, AppSettings appSettings)
        {
            DependencyInjection.Register(services);
            DependencyInjectionRabbitMq.Register(services, appSettings);
            DependencyInjectionMinio.Register(services, appSettings);
        }
    }
}