using MicroNet.System.Core.Clients;
using MicroNet.System.Core.Repositories;
using MicroNet.System.Infrastructure.Clients;
using MicroNet.System.Infrastructure.Data;
using MicroNet.System.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.System.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<SystemContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SystemConnection"),
                    b => b.MigrationsAssembly(typeof(SystemContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<ICompanySetupRepository, CompanySetupRepository>();

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
