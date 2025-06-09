using MicroNet.Loan.Core.Repositories;
using MicroNet.Loan.Infrastructure.Data;
using MicroNet.Loan.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Loan.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<LoanContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("LoanContext"),
                    b => b.MigrationsAssembly(typeof(LoanContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<ILoanRequestRepository, LoanRequestRepository>();
            
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
