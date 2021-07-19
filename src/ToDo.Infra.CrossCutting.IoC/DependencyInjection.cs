using FavoDeMel.Infra.Application.Services;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using ToDo.Application.Interfaces;
using ToDo.Application.Services;
using ToDo.Domain.Commands;
using ToDo.Domain.Core.Bus;
using ToDo.Domain.Core.Events;
using ToDo.Domain.Events;
using ToDo.Domain.Interfaces;
using ToDo.Infra.Data.Contexts;
using ToDo.Infra.Data.MongoDB.Context;
using ToDo.Infra.Data.MongoDB.Interfaces;
using ToDo.Infra.Data.MongoDB.Repository;
using ToDo.Infra.Data.Repository.EventSourcing;
using ToDo.Infra.Data.Repository.Repositories;
using ToDo.Infra.Data.Repository.Repositories.EventSourcing;

namespace ToDo.Infra.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static void Register(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IRabbitMQService, RabbitMQService>();
            services.AddScoped<IMinioService, MinioService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<BookCreateEvent>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookUpdateEvent>, BookEventHandler>();
            services.AddScoped<INotificationHandler<BookDeleteEvent>, BookEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<BookCreateCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<BookUpdateCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<BookDeleteCommand, ValidationResult>, BookCommandHandler>();

            // Infra - Data
            services.AddScoped<IBookRepository, BookRepository>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, EventStore>();

            // Repository - MongoDB
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddScoped<IMongoRepository, MongoRepository>();
        }
    }
}
