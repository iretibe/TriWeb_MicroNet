using MicroNet.Employee.Core.Clients;
using MicroNet.Employee.Core.Repositories;
using MicroNet.Employee.Infrastructure.Clients;
using MicroNet.Employee.Infrastructure.Data;
using MicroNet.Employee.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Employee.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<EmployeeContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("EmployeeContext"),
                    b => b.MigrationsAssembly(typeof(EmployeeContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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
