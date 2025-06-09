using MicroNet.User.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(x => x.UserId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.UserGroupId).IsRequired();
            builder.Property(x => x.BranchId).IsRequired();
            builder.Property(x => x.RoleName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.OwnsOne(x => x.AuditInfo, ai =>
            {
                ai.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
                ai.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt");
                ai.Property(x => x.DeletedAt).HasColumnName("DeletedAt");

                ai.Property(x => x.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(100);
                ai.Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").HasMaxLength(100);
                ai.Property(x => x.DeletedBy).HasColumnName("DeletedBy").HasMaxLength(100);
            });
        }
    }
}
