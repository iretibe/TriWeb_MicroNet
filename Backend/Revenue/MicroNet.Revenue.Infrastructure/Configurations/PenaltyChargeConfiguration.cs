using MicroNet.Revenue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Revenue.Infrastructure.Configurations
{
    public class PenaltyChargeConfiguration : IEntityTypeConfiguration<PenaltyCharge>
    {
        public void Configure(EntityTypeBuilder<PenaltyCharge> builder)
        {
            builder.ToTable("PenaltyCharges");
            builder.HasKey(p => p.Id);
            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(p => p.AccountNumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");

            builder.OwnsOne(p => p.Reason, r =>
            {
                r.Property(x => x.Code).HasMaxLength(20);
                r.Property(x => x.Description).HasMaxLength(100);
            });

            builder.Property(p => p.ChargedAt).IsRequired();

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
