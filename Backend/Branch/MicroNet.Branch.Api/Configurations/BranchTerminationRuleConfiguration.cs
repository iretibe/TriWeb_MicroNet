using MicroNet.Branch.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Branch.Api.Configurations
{
    public class BranchTerminationRuleConfiguration : IEntityTypeConfiguration<BranchTerminationRule>
    {
        public void Configure(EntityTypeBuilder<BranchTerminationRule> builder)
        {
            builder.ToTable("BranchTerminationRules");

            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.OwnsOne(x => x.AuditInfo);
        }
    }
}
