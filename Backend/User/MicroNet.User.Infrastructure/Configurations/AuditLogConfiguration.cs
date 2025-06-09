using MicroNet.User.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(x => x.AuditDate).IsRequired();
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Method).IsRequired().HasMaxLength(10);
            builder.Property(x => x.EntityType).IsRequired().HasMaxLength(100);
        }
    }
}
