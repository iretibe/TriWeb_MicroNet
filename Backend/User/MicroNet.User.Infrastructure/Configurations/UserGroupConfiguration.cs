using MicroNet.User.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(ug => ug.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(ug => ug.UserGroupName).IsRequired().HasMaxLength(100);
            builder.Property(ug => ug.IsActive).IsRequired();
            builder.Property(ug => ug.BranchId).IsRequired();

            builder.OwnsOne(ug => ug.WorkingHours, w =>
            {
                w.Property(p => p.Start).IsRequired();
                w.Property(p => p.End).IsRequired();
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

            builder.Property(ug => ug.IsDeleted).HasDefaultValue(false);
        }
    }
}
