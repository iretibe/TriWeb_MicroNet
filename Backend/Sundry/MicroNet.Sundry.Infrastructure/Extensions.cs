using MicroNet.Sundry.Core.Repositories;
using MicroNet.Sundry.Core.Repositories.Outbox;
using MicroNet.Sundry.Infrastructure.Data;
using MicroNet.Sundry.Infrastructure.Repositories;
using MicroNet.Sundry.Infrastructure.Repositories.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Sundry.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<AccountingContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AccountingContext"),
                    b => b.MigrationsAssembly(typeof(AccountingContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<IAccountingRepository, AccountingRepository>();
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
