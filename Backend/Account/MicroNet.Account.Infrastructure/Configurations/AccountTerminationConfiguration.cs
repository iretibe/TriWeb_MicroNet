using MicroNet.Account.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Account.Infrastructure.Configurations
{
    public class AccountTerminationConfiguration : IEntityTypeConfiguration<AccountTermination>
    {
        public void Configure(EntityTypeBuilder<AccountTermination> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.OwnsOne(x => x.TerminatedAccount);
            builder.OwnsOne(x => x.AuditInfo);
        }
    }
}
