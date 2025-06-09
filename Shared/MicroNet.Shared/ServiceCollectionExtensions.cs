using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.Shared.CQRS.Dispatchers.Commands;
using MicroNet.Shared.CQRS.Dispatchers.Events;
using MicroNet.Shared.CQRS.Dispatchers.Queries;
using MicroNet.Shared.CQRS.Events;
using MicroNet.Shared.Messaging.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MicroNet.Shared
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetCallingAssembly();
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetCallingAssembly();
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }

        public static IServiceCollection AddInMemoryCommandDispatcher(this IServiceCollection services)
        {
            return services.AddScoped<ICommandDispatcher, InMemoryCommandDispatcher>();
        }

        public static IServiceCollection AddInMemoryQueryDispatcher(this IServiceCollection services)
        {
            return services.AddScoped<IQueryDispatcher, InMemoryQueryDispatcher>();
        }

        public static IServiceCollection AddInMemoryDispatcher(this IServiceCollection services)
        {
            return services.AddScoped<IDispatcher, InMemoryDispatcher>();
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddInMemoryEventDispatcher(this IServiceCollection services)
        {
            services.AddScoped<IEventDispatcher, InMemoryEventDispatcher>();

            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddRabbitMqEventDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
            services.AddSingleton<IEventDispatcher, RabbitMqEventDispatcher>();
            
            return services;
        }

        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
            services.AddSingleton<IMessageBroker, RabbitMQMessageBroker>();

            return services;
        }
    }
}
