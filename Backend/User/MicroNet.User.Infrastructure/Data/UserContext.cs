using MicroNet.User.Core.Entities;
using MicroNet.User.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupMenu> UserGroupMenus { get; set; }
        public DbSet<UserSubGroupMenu> UserSubGroupMenus { get; set; }
        public DbSet<UserGroupWorkingDay> UserGroupWorkingDays { get; set; }
        public DbSet<UserMenuAccess> UserMenuAccesses { get; set; }
        public DbSet<PasswordPolicy> PasswordPolicies { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<DomainEventLog> DomainEventLogs { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("user");

            modelBuilder.ApplyConfiguration(new UserGroupConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupMenuConfiguration());
            modelBuilder.ApplyConfiguration(new UserSubGroupMenuConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupWorkingDayConfiguration());
            modelBuilder.ApplyConfiguration(new UserMenuAccessConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordPolicyConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
            modelBuilder.ApplyConfiguration(new DomainEventLogConfiguration());
            modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
