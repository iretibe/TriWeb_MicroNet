using MicroNet.Sundry.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Sundry.Infrastructure.Configurations
{
    public class AccountDefinitionConfiguration : IEntityTypeConfiguration<Accounting>
    {
        public void Configure(EntityTypeBuilder<Accounting> builder)
        {
            builder.ToTable("Accounting");
            builder.HasKey(x => x.Id);
            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Category).IsRequired();
            builder.OwnsOne(x => x.AuditInfo);
        }
    }
}
