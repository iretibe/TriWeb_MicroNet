using MicroNet.Revenue.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Revenue.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(t => t.Reference).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(t => t.DepositorName).IsRequired().HasMaxLength(200);
            builder.Property(t => t.DestinationType).IsRequired().HasMaxLength(200);

            builder.OwnsOne(x => x.AuditInfo, audit =>
            {
                audit.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
                audit.Property(a => a.CreatedAt).HasColumnName("CreatedAt");
                audit.Property(a => a.UpdatedBy).HasColumnName("UpdatedBy");
                audit.Property(a => a.UpdatedAt).HasColumnName("UpdatedAt");
                audit.Property(a => a.DeletedBy).HasColumnName("DeletedBy");
                audit.Property(a => a.DeletedAt).HasColumnName("DeletedAt");
            });

            builder.OwnsOne(x => x.Receiver, receiver =>
            {
                receiver.Property(a => a.AccountNumber).HasColumnName("AccountNumber");
                receiver.Property(a => a.AccountName).HasColumnName("AccountName");
            });

            builder.OwnsOne(x => x.DepositorId, depositor =>
            {
                depositor.Property(a => a.IdType).HasColumnName("IdType");
                depositor.Property(a => a.IdNumber).HasColumnName("IdNumber");
            });
        }
    }
}
