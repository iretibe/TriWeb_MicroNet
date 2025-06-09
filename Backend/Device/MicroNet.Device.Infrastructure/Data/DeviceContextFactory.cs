using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Device.Infrastructure.Data
{
    public class DeviceContextFactory : IDesignTimeDbContextFactory<DeviceContext>
    {
        public DeviceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeviceContext>();
            optionsBuilder.UseSqlServer("Server=PSL-SYESSOUFOU\\SQL2022;Database=DMA_MicroNet_DB;User ID=sa;Password=Persol@2023;Trusted_Connection=False;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new DeviceContext(optionsBuilder.Options);
        }
    }
}
