using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroNet.Loan.Infrastructure.Data
{
    public class LoanContextFactory : IDesignTimeDbContextFactory<LoanContext>
    {
        public LoanContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LoanContext>();
            optionsBuilder.UseSqlServer("Server=PSL-SYESSOUFOU\\SQL2022;Database=DMA_MicroNet_DB;User ID=sa;Password=Persol@2023;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new LoanContext(optionsBuilder.Options);
        }
    }
}
