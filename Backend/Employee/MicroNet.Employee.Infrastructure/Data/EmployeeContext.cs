using Microsoft.EntityFrameworkCore;

namespace MicroNet.Employee.Infrastructure.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Core.Entities.Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("employee");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
