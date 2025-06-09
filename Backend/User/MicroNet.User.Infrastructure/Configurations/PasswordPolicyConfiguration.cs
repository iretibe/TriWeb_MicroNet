using MicroNet.User.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Configurations
{
    public class PasswordPolicyConfiguration : IEntityTypeConfiguration<PasswordPolicy>
    {
        public void Configure(EntityTypeBuilder<PasswordPolicy> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.Property(x => x.PolicyName).IsRequired().HasMaxLength(100);

            builder.OwnsOne(x => x.Requirements, r =>
            {
                r.Property(p => p.RequiredLength).IsRequired();
                r.Property(p => p.RequireNonAlphanumeric).IsRequired();
                r.Property(p => p.RequireDigit).IsRequired();
                r.Property(p => p.RequireLowercase).IsRequired();
                r.Property(p => p.RequireUppercase).IsRequired();
                r.Property(p => p.RequireUniqueChars).IsRequired();
            });

            builder.Property(x => x.UserGroupId).IsRequired();

            builder.OwnsOne(d => d.AuditInfo, ai =>
            {
                ai.Property(x => x.CreatedAt).HasColumnName("CreatedAt");
                ai.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt");
                ai.Property(x => x.DeletedAt).HasColumnName("DeletedAt");
                ai.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
                ai.Property(x => x.UpdatedBy).HasColumnName("UpdatedBy");
                ai.Property(x => x.DeletedBy).HasColumnName("DeletedBy");
            });

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
