using MicroNet.Account.Core.Repositories;
using MicroNet.Account.Infrastructure.Data;
using MicroNet.Account.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Account.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AccountContext"),
                    b => b.MigrationsAssembly(typeof(AccountContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<IAccountTerminationRepository, AccountTerminationRepository>();
            services.AddScoped<IAccountTransferRepository, AccountTransferRepository>();
            services.AddScoped<IWithdrawalRepository, WithdrawalRepository>();

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
