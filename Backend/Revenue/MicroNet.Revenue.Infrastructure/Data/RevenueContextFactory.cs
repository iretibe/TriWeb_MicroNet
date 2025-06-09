using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroNet.Revenue.Infrastructure.Data
{
    public class RevenueContextFactory : IDesignTimeDbContextFactory<RevenueContext>
    {
        public RevenueContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RevenueContext>();
            optionsBuilder.UseSqlServer("Server=PSL-SYESSOUFOU\\SQL2022;Database=DMA_MicroNet_DB;User ID=sa;Password=Persol@2023;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new RevenueContext(optionsBuilder.Options);
        }
    }
}
