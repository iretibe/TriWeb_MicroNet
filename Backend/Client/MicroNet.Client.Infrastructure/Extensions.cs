using MicroNet.Client.Core.Clients;
using MicroNet.Client.Core.Repositories;
using MicroNet.Client.Infrastructure.Clients;
using MicroNet.Client.Infrastructure.Data;
using MicroNet.Client.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Client.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<ClientContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ClientContext"),
                    b => b.MigrationsAssembly(typeof(ClientContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddHttpClient("UserService", client =>
            {
                client.BaseAddress = new Uri(configuration["OtherServiceUrls:UserServiceUrl"]!);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddScoped<IAuditLogServiceClient, AuditLogServiceClient>();

            return services;
        }
    }
}
