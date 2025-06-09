using MicroNet.Device.Core.Repositories;
using MicroNet.Device.Infrastructure.Data;
using MicroNet.Device.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNet.Device.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<DeviceContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DeviceConnection"),
                    b => b.MigrationsAssembly(typeof(DeviceContext).Assembly.FullName)
                ));

            // Register the repository
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }
}
