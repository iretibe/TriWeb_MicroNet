using MicroNet.User.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class UserSubGroupMenuConfiguration : IEntityTypeConfiguration<UserSubGroupMenu>
    {
        public void Configure(EntityTypeBuilder<UserSubGroupMenu> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(x => x.UserGroupId).IsRequired();
            builder.Property(x => x.MenuId).IsRequired();
            builder.Property(x => x.SubMenuId).IsRequired();
            builder.Property(x => x.IsChecked).IsRequired();
        }
    }
}
