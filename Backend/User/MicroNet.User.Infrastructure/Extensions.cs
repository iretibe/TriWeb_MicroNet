using MicroNet.User.Core.Repositories;
using MicroNet.User.Infrastructure.Data;
using MicroNet.User.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroNet.User.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with the connection string from configuration
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("UserConnection"),
                    b => b.MigrationsAssembly(typeof(UserContext).Assembly.FullName)
                ));

            services.AddDistributedMemoryCache();

            // Register the repository
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAuditLogRepository, AuditLogRepository>()
                .AddScoped<IEmailRepository, EmailRepository>()
                .AddScoped<IUserPermissionRepository, UserPermissionRepository>()
                .AddScoped<IPasswordPolicyRepository, PasswordPolicyRepository>()
                .AddScoped<IUserGroupRepository, UserGroupRepository>()
                .AddScoped<IUserAccessRepository, UserAccessRepository>();

            return services;
        }
    }
}
