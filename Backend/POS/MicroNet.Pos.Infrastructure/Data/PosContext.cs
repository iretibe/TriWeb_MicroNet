using MicroNet.Pos.Core.Entities;
using MicroNet.Pos.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Pos.Infrastructure.Data
{
    public class PosContext : DbContext
    {
        public PosContext(DbContextOptions<PosContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("pos");
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
