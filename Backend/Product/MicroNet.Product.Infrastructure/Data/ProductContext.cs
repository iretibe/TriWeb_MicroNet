using MicroNet.Product.Core.Entities;
using MicroNet.Product.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Product.Infrastructure.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Core.Entities.Product> Products => Set<Core.Entities.Product>();
        public DbSet<Loan> Loans => Set<Loan>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("product");

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
