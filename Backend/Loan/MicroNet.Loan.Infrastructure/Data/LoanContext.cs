using MicroNet.Loan.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Loan.Infrastructure.Data
{
    public class LoanContext : DbContext
    {
        public LoanContext(DbContextOptions<LoanContext> options) : base(options)
        {
        }

        public DbSet<LoanRequest> LoanRequests { get; set; } = default!;
        public DbSet<DomainEventLog> DomainEventLogs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("loan");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LoanContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
