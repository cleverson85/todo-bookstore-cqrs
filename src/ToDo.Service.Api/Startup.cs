using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.Domain.Settings;
using ToDo.Service.Api.Configurations;
using ToDo.Service.Api.Filters;
using ToDo.Service.Api.Middlewares;

namespace ToDo.Service.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = new AppSettings(Configuration);
            services.AddSingleton(options => appSettings);

            // WebAPI Config
            services.AddControllers(options => options.Filters.Add(typeof(CacheResourceFilter)));

            // Setting DBContexts
            services.AddDatabaseConfiguration(appSettings.DefaultConnectionString);

            // AutoMapper Settings
            services.AddAutoMapperConfiguration();

            // Swagger Config
            services.AddSwaggerConfiguration();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            services.AddDependencyInjectionConfiguration(appSettings);

            services.AddScoped<CacheResourceFilter>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = appSettings.RedisConnectionString;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
             .UseHttpsRedirection()
             .UseRouting()
             .UseCors(c =>
             {
                 c.AllowAnyHeader();
                 c.AllowAnyMethod();
                 c.AllowAnyOrigin();
             })
             .UseMiddleware(typeof(ErrorHandlingMiddleware))
             .UseAuthentication()
             .UseAuthorization()
             .UseEndpoints(options =>
             {
                 options.MapControllers();
             })
             .UseSwaggerSetup();
        }
    }
}
