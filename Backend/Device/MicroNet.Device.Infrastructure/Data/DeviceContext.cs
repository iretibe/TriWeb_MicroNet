using MicroNet.Device.Core.Configurations;
using MicroNet.Device.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Device.Infrastructure.Data
{
    public class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> options) : base(options) { }

        public DbSet<Core.Entities.Device> Devices { get; set; }
        public DbSet<Core.Entities.DomainEventLog> DomainEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("device");

            modelBuilder.ApplyConfiguration(new DeviceConfiguration());

            modelBuilder.Entity<DomainEventLog>(entity =>
            {
                entity.ToTable("DomainEventLogs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EventType).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Payload).IsRequired();
                entity.Property(e => e.AggregateType).HasMaxLength(100);
            });
        }
    }
}
