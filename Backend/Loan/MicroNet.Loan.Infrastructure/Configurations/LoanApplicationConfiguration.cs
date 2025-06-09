using MicroNet.Loan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Loan.Infrastructure.Configurations
{
    public class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanRequest>
    {
        public void Configure(EntityTypeBuilder<LoanRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(d => d.Id)
                  .HasConversion(
                      id => id.Id,
                      guid => new AggregateId(guid))
                  .ValueGeneratedNever();

            builder.Property(x => x.AccountNumber).IsRequired();
            builder.Property(x => x.ClientName).IsRequired();
            builder.Property(x => x.Branch).IsRequired();
            builder.Property(x => x.LoanType).IsRequired();
            builder.Property(x => x.InterestRate).HasPrecision(18, 2);
            builder.Property(x => x.RepaymentPeriod);
            builder.Property(x => x.MaximumAmount).HasPrecision(18, 2);
            builder.Property(x => x.RequestedPrincipal).HasPrecision(18, 2);
            builder.Property(x => x.RiskMargin).HasPrecision(18, 2);
            builder.Property(x => x.InsuranceAmount).HasPrecision(18, 2);
            builder.Property(x => x.DisbursementMedium);
            builder.Property(x => x.Status);
            builder.Property(x => x.ReviewerComment);

            builder.OwnsMany(x => x.SupportingDocuments, doc =>
            {
                doc.Property(d => d.FileName).IsRequired();
                doc.Property(d => d.FileUrl).IsRequired();
            });

            builder.OwnsOne(x => x.AuditInfo);
        }
    }
}
