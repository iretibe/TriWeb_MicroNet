using MicroNet.Branch.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Branch.Api.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Entities.Branch>
    {
        public void Configure(EntityTypeBuilder<Entities.Branch> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                  .HasConversion(
                      id => id.Id,
                      guid => new AggregateId(guid))
                  .ValueGeneratedNever();

            builder.Property(x => x.BranchCode).IsRequired().HasMaxLength(20);
            builder.Property(x => x.BranchName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Region).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ManagerName).IsRequired().HasMaxLength(100);

            builder.OwnsOne(x => x.PhysicalAddress, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(100);
                address.Property(a => a.PostalAddress).HasColumnName("PostalAddress").HasMaxLength(50);
            });

            builder.OwnsMany(x => x.ProductSummaries, ps =>
            {
                ps.WithOwner().HasForeignKey("BranchId");

                ps.Property<Guid>("Id");
                ps.HasKey("Id");

                ps.Property(p => p.NumberOfLoans);
                ps.Property(p => p.TotalLoanAmount);
                ps.Property(p => p.TotalInterest);
                ps.Property(p => p.TotalRepayment);
                ps.Property(p => p.ProcessingFees);
                ps.Property(p => p.PenaltyCharges);
                ps.Property(p => p.TotalLoanBalance);

                ps.Property(p => p.ProductAmount);
                ps.Property(p => p.Interest);
                ps.Property(p => p.Withdrawal);
                ps.Property(p => p.ManagementFees);
                ps.Property(p => p.Balance);
            });

            builder.OwnsOne(x => x.AuditInfo, audit =>
            {
                audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                audit.Property(a => a.CreatedAt).HasColumnName("CreatedAt");
                audit.Property(a => a.UpdatedBy).HasColumnName("UpdatedBy");
                audit.Property(a => a.UpdatedAt).HasColumnName("UpdatedAt");
            });
        }
    }
}