using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroNet.Account.Infrastructure.Data
{
    public class AccountContextFactory : IDesignTimeDbContextFactory<AccountContext>
    {
        public AccountContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AccountContext>();
            optionsBuilder.UseSqlServer("Server=PSL-SYESSOUFOU\\SQL2022;Database=DMA_MicroNet_DB;User ID=sa;Password=Persol@2023;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new AccountContext(optionsBuilder.Options);
        }
    }
}
