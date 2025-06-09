using MicroNet.Revenue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Revenue.Infrastructure.Configurations
{
    public class InterestDistributionConfiguration : IEntityTypeConfiguration<InterestDistribution>
    {
        public void Configure(EntityTypeBuilder<InterestDistribution> builder)
        {
            builder.ToTable("InterestDistributions");
            builder.HasKey(i => i.Id);
            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.OwnsOne(i => i.Period, p =>
            {
                p.Property(x => x.StartDate).HasColumnName("StartDate");
                p.Property(x => x.EndDate).HasColumnName("EndDate");
            });

            builder.Property(i => i.TotalInterest).HasColumnType("decimal(18,2)");
            builder.Property(i => i.DistributedAt).IsRequired();

            builder.OwnsMany(i => i.DistributedTo, a =>
            {
                a.WithOwner().HasForeignKey("InterestDistributionId");
                a.Property(x => x.AccountNumber).HasMaxLength(20);
                a.Property(x => x.ShareAmount).HasColumnType("decimal(18,2)");
                a.ToTable("InterestDistributionAccounts");
            });

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
