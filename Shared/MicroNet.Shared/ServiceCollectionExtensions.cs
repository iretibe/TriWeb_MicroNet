﻿using Consul;
using MicroNet.Shared.Consul.ServiceDiscovery;
using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.Shared.CQRS.Dispatchers.Commands;
using MicroNet.Shared.CQRS.Dispatchers.Events;
using MicroNet.Shared.CQRS.Dispatchers.Queries;
using MicroNet.Shared.CQRS.Events;
using MicroNet.Shared.Messaging.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public static IServiceCollection AddConsulServiceDiscovery(this IServiceCollection services, string consulAddress)
        {
            services.AddSingleton<IConsulClient>(sp => new ConsulClient(config =>
            {
                config.Address = new Uri(consulAddress);
            }));

            services.AddSingleton<ConsulServiceLocator>();

            return services;
        }

        public static IServiceCollection AddConsulFabio(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p =>
            {
                var address = configuration["consul:Address"] ?? "http://localhost:8500";
                return new ConsulClient(cfg => cfg.Address = new Uri(address));
            });

            services.AddSingleton<ConsulServiceRegistry>();
            return services;
        }

        public static async Task UseConsulFabio(this IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            var registry = app.ApplicationServices.GetRequiredService<ConsulServiceRegistry>();
            await registry.RegisterAsync();

            lifetime.ApplicationStopping.Register(() =>
            {
                // Fire-and-forget de-registration on shutdown
                Task.Run(() => registry.DeregisterAsync());
            });
        }
    }
}
