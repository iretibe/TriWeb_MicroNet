using MicroNet.Sundry.Core.Entities;
using MicroNet.Sundry.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Sundry.Infrastructure.Data
{
    public class AccountingContext : DbContext
    {
        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options) { }

        public DbSet<Accounting> Accountings { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sundry");

            modelBuilder.ApplyConfiguration(new AccountDefinitionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
