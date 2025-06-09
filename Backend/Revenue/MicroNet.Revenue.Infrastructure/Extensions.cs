using MicroNet.Revenue.Core.Repositories;
using MicroNet.Revenue.Infrastructure.Data;
using MicroNet.Revenue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Revenue.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<RevenueContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("RevenueContext"),
                    b => b.MigrationsAssembly(typeof(RevenueContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<IInterestDistributionRepository, InterestDistributionRepository>();
            services.AddScoped<IManagementFeeRepository, ManagementFeeRepository>();
            services.AddScoped<IPenaltyChargeRepository, PenaltyChargeRepository>();
            services.AddScoped<IRevenueReversalRepository, RevenueReversalRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

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
