using Microsoft.EntityFrameworkCore;
using DatabaseLayer.Entities;

namespace DatabaseLayer.InsuranceContext
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) { }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>()
                .Property(p => p.CoverageAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Claim>()
                .Property(c => c.ClaimAmount)
                .HasPrecision(12, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
