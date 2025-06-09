using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroNet.Sundry.Infrastructure.Data
{
    public class AccountingContextFactory : IDesignTimeDbContextFactory<AccountingContext>
    {
        public AccountingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountingContext>();
            optionsBuilder.UseSqlServer("Server=PSL-SYESSOUFOU\\SQL2022;Database=DMA_MicroNet_DB;User ID=sa;Password=Persol@2023;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new AccountingContext(optionsBuilder.Options);
        }
    }
}
