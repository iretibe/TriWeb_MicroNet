using MicroNet.Client.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Client.Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Core.Entities.Client>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(p => p.Street).HasMaxLength(150);
                a.Property(p => p.City).HasMaxLength(100);
                a.Property(p => p.State).HasMaxLength(100);
                a.Property(p => p.ZipCode).HasMaxLength(20);
            });

            builder.OwnsOne(x => x.KYC);

            builder.OwnsOne(x => x.AuditInfo);
        }
    }
}
