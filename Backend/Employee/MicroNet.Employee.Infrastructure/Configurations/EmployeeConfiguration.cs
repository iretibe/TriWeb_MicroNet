using MicroNet.Employee.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroNet.Employee.Infrastructure.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Core.Entities.Employee>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                       id => id.Id,
                       guid => new AggregateId(guid))
                   .ValueGeneratedNever();

            builder.OwnsOne(e => e.ContactPerson);
            builder.OwnsOne(e => e.AuditInfo);
            builder.Property(e => e.EmployeeNumber).IsRequired();
        }
    }
}
