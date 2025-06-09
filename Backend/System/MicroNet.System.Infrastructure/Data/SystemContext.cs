using MicroNet.System.Core.Entities;
using MicroNet.System.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.System.Infrastructure.Data
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {
        }

        public DbSet<CompanySetup> CompanySetups { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("system");

            modelBuilder.ApplyConfiguration(new CompanySetupConfiguration());

            // Apply other configurations if needed
            base.OnModelCreating(modelBuilder);
        }
    }
}
