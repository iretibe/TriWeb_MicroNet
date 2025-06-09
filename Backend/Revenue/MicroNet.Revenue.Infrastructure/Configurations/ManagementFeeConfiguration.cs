using MicroNet.Revenue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Revenue.Infrastructure.Configurations
{
    public class ManagementFeeConfiguration : IEntityTypeConfiguration<ManagementFee>
    {
        public void Configure(EntityTypeBuilder<ManagementFee> builder)
        {
            builder.ToTable("ManagementFees");
            builder.HasKey(m => m.Id);
            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(m => m.AccountNumber).IsRequired().HasMaxLength(20);

            builder.OwnsOne(m => m.Fee, f =>
            {
                f.Property(x => x.Type).HasMaxLength(50);
                f.Property(x => x.RateOrAmount).HasColumnType("decimal(18,2)");
                f.Property(x => x.Frequency).HasMaxLength(50);
            });

            builder.Property(m => m.CalculatedAmount).HasColumnType("decimal(18,2)");
            builder.Property(m => m.ChargedAt).IsRequired();

            builder.OwnsOne(x => x.AuditInfo, audit =>
            {
                audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                audit.Property(a => a.CreatedAt).HasColumnName("CreatedAt");
                audit.Property(a => a.UpdatedBy).HasColumnName("UpdatedBy");
                audit.Property(a => a.UpdatedAt).HasColumnName("UpdatedAt");
                audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
                audit.Property(a => a.DeletedAt).HasColumnName("DeletedAt");
            });
        }
    }
}
