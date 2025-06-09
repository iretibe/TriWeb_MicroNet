using MicroNet.Product.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Product.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Core.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                  .HasConversion(
                      id => id.Id,
                      guid => new AggregateId(guid))
                  .ValueGeneratedNever();

            builder.Property(x => x.ProductCode).IsRequired().HasMaxLength(20);
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(100);

            builder.OwnsOne(d => d.AuditInfo, ai =>
            {
                ai.Property(x => x.CreatedAt).HasColumnName("CreatedAt");
                ai.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt");
                ai.Property(x => x.DeletedAt).HasColumnName("DeletedAt");
                ai.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
                ai.Property(x => x.UpdatedBy).HasColumnName("UpdatedBy");
                ai.Property(x => x.DeletedBy).HasColumnName("DeletedBy");
            });
        }
    }
}
