using MicroNet.Pos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Pos.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(d => d.Id)
                  .HasConversion(
                      id => id.Id,
                      guid => new AggregateId(guid))
                  .ValueGeneratedNever();

            builder.OwnsOne(x => x.AuditInfo, ai =>
            {
                ai.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                ai.Property(a => a.CreatedAt).HasColumnName("CreatedAt");
                ai.Property(a => a.UpdatedBy).HasColumnName("UpdatedBy");
                ai.Property(a => a.UpdatedAt).HasColumnName("UpdatedAt");
            });

            builder.Property(x => x.TransactionType).IsRequired();
            builder.Property(x => x.AccountNumber).IsRequired();
            builder.Property(x => x.AgentCode).IsRequired();
        }
    }
}
