using MicroNet.Account.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Account.Infrastructure.Configurations
{
    public class AccountTransferConfiguration : IEntityTypeConfiguration<AccountTransfer>
    {
        public void Configure(EntityTypeBuilder<AccountTransfer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.OwnsOne(x => x.SourceAccount);
            builder.OwnsOne(x => x.AuditInfo);
        }
    }
}
