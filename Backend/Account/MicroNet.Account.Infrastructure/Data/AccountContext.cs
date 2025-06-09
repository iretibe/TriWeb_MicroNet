using MicroNet.Account.Core.Entities;
using MicroNet.Account.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Account.Infrastructure.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        public DbSet<AccountTransfer> AccountTransfers { get; set; }
        public DbSet<AccountTermination> AccountTerminations { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("account");

            modelBuilder.ApplyConfiguration(new AccountTransferConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTerminationConfiguration());
            modelBuilder.ApplyConfiguration(new WithdrawalConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
