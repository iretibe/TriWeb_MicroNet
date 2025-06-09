using MicroNet.Revenue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Revenue.Infrastructure.Configurations
{
    public class RevenueReversalConfiguration : IEntityTypeConfiguration<RevenueReversal>
    {
        public void Configure(EntityTypeBuilder<RevenueReversal> builder)
        {
            builder.ToTable("RevenueReversals");
            builder.HasKey(r => r.Id);
            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(r => r.OriginalTransactionId).IsRequired();
            builder.Property(r => r.Amount).HasColumnType("decimal(18,2)");

            builder.OwnsOne(r => r.Reason, rr =>
            {
                rr.Property(x => x.Code).HasMaxLength(20);
                rr.Property(x => x.Description).HasMaxLength(100);
            });

            builder.Property(r => r.ReversedBy).HasMaxLength(100);
            builder.Property(r => r.ReversedAt).IsRequired();

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
