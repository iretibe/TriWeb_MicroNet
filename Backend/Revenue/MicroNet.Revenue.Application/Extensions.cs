using MicroNet.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNet.Revenue.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(Extensions).Assembly;

            services
                .AddCommandHandlers(assembly)
                .AddQueryHandlers(assembly)
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddEventHandlers()
                .AddInMemoryEventDispatcher();

            return services;
        }
    }
}
