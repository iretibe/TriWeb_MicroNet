using MicroNet.Revenue.Core.Entities;
using MicroNet.Revenue.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Revenue.Infrastructure.Data
{
    public class RevenueContext : DbContext
    {
        public RevenueContext(DbContextOptions<RevenueContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<InterestDistribution> InterestDistributions { get; set; }
        public DbSet<ManagementFee> ManagementFees { get; set; }
        public DbSet<PenaltyCharge> PenaltyCharges { get; set; }
        public DbSet<RevenueReversal> RevenueReversals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("revenue");

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new InterestDistributionConfiguration());
            modelBuilder.ApplyConfiguration(new ManagementFeeConfiguration());
            modelBuilder.ApplyConfiguration(new PenaltyChargeConfiguration());
            modelBuilder.ApplyConfiguration(new RevenueReversalConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
