using MicroNet.User.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class UserMenuAccessConfiguration : IEntityTypeConfiguration<UserMenuAccess>
    {
        public void Configure(EntityTypeBuilder<UserMenuAccess> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(x => x.UserId).IsRequired().HasMaxLength(100);
            builder.Property(x => x.MenuId).IsRequired();
            builder.Property(x => x.IsChecked).IsRequired();

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
