using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext(DbContextOptions<MenuContext> options) : base(options) { }

        public DbSet<Entities.Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("menu");

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
