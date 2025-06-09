using MicroNet.Pos.Core.Repositories;
using MicroNet.Pos.Core.Repositories.Outbox;
using MicroNet.Pos.Infrastructure.Data;
using MicroNet.Pos.Infrastructure.Repositories;
using MicroNet.Pos.Infrastructure.Repositories.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Pos.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<PosContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("PosContext"),
                    b => b.MigrationsAssembly(typeof(PosContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();

            services.AddHttpClient("UserService", client =>
            {
                client.BaseAddress = new Uri(configuration["OtherServiceUrls:UserServiceUrl"]!);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            //services.AddScoped<IAuditLogServiceClient, AuditLogServiceClient>();

            return services;
        }
    }
}
