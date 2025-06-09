using MicroNet.Client.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Client.Infrastructure.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options) { }

        public DbSet<Core.Entities.Client> Clients => Set<Core.Entities.Client>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("client");

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
        }
    }
}
