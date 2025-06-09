using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class UserGroupWorkingDayConfiguration : IEntityTypeConfiguration<UserGroupWorkingDay>
    {
        public void Configure(EntityTypeBuilder<UserGroupWorkingDay> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(x => x.UserGroupId).IsRequired();
            builder.Property(x => x.DayOfWeek).IsRequired();
        }
    }
}
