using MicroNet.Product.Core.Clients;
using MicroNet.Product.Core.Repositories;
using MicroNet.Product.Infrastructure.Clients;
using MicroNet.Product.Infrastructure.Data;
using MicroNet.Product.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MicroNet.Product.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ProductContext"),
                    b => b.MigrationsAssembly(typeof(ProductContext).Assembly.FullName)
                ));

            // Register the repository
            services
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ILoanRepository, LoanRepository>();

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
