using MicroNet.Branch.Api.Configurations;
using MicroNet.Branch.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Branch.Api.Data
{
    public class BranchContext : DbContext
    {
        public DbSet<Entities.Branch> Branches { get; set; }
        public DbSet<BranchTerminationRule> BranchTerminationRules { get; set; }

        public BranchContext(DbContextOptions<BranchContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("branch");

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new BranchTerminationRuleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
