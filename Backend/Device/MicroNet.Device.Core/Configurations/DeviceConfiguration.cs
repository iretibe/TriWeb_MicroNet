using MicroNet.Device.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Device.Core.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Entities.Device>
    {
        public void Configure(EntityTypeBuilder<Entities.Device> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.OwnsOne(d => d.Code, c =>
            {
                c.Property(x => x.Value).HasColumnName("Code").IsRequired();
            });

            builder.OwnsOne(d => d.Name, n =>
            {
                n.Property(x => x.Value).HasColumnName("Name").IsRequired();
            });

            builder.OwnsOne(d => d.Description, dsc =>
            {
                dsc.Property(x => x.Value).HasColumnName("Description");
            });

            builder.OwnsOne(d => d.Notes, nt =>
            {
                nt.Property(x => x.Value).HasColumnName("Notes");
            });

            builder.OwnsOne(d => d.AuditInfo, ai =>
            {
                ai.Property(x => x.CreatedAt).HasColumnName("CreatedAt");
                ai.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt");
                ai.Property(x => x.DeletedAt).HasColumnName("DeletedAt");
                ai.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
                ai.Property(x => x.UpdatedBy).HasColumnName("UpdatedBy");
                ai.Property(x => x.DeletedBy).HasColumnName("DeletedBy");
            });

            builder.Property(d => d.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
