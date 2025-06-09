using MicroNet.User.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class DomainEventLogConfiguration : IEntityTypeConfiguration<DomainEventLog>
    {
        public void Configure(EntityTypeBuilder<DomainEventLog> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.EventType).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Payload).IsRequired();
            builder.Property(e => e.AggregateType).HasMaxLength(100);
        }
    }
}
