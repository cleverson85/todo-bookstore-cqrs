using Microsoft.Extensions.DependencyInjection;
using Minio;
using ToDo.Application.Services;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Settings;

namespace ToDo.Infra.CrossCutting.IoC
{
    public static class DependencyInjectionMinio
    {
        public static void Register(IServiceCollection service, AppSettings appSettings)
        {
            MinioSettings settings = appSettings.MinioSettings;

            service.AddScoped<MinioClient>(config =>
            {
                var client = new MinioClient(settings.Endpoint, settings.AccessKey, settings.SecretKey).WithSSL();
                client.SetTraceOn();

                return client;
            });

            service.AddScoped<IMinioService, MinioService>();
        }
    }
}
