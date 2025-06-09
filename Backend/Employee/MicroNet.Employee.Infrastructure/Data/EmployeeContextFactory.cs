using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroNet.Employee.Infrastructure.Data
{
    public class EmployeeContextFactory : IDesignTimeDbContextFactory<EmployeeContext>
    {
        public EmployeeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeContext>();
            optionsBuilder.UseSqlServer("Server=PSL-SYESSOUFOU\\SQL2022;Database=DMA_MicroNet_DB;User ID=sa;Password=Persol@2023;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new EmployeeContext(optionsBuilder.Options);
        }
    }
}
