using GreenPipes; // for call to Immediate
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using ToDo.Domain.Settings;
using ToDo.Infra.Data.MongoDB.Consumer;

namespace ToDo.Infra.CrossCutting.IoC
{
    public static class DependencyInjectionRabbitMq
    {
        public static void Register(IServiceCollection services, AppSettings appSettings)
        {
            services.AddMassTransit(bus =>
            {
                bus.AddConsumer(typeof(FaultConsumer));
                bus.AddConsumer<BookEventConsumer>(c => c.UseMessageRetry(c => c.Interval(5, TimeSpan.FromMinutes(5))));

                bus.SetKebabCaseEndpointNameFormatter();

                bus.UsingRabbitMq((context, config) =>
                {
                    config.Host(appSettings.RabbitMQSettings.Url);
                    config.ConfigureEndpoints(context);
                });
            });

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddMassTransitHostedService();
        }
    }
}