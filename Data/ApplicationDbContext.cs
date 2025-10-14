using Microsoft.EntityFrameworkCore;
using SupplyChainTransparency.Models;

namespace SupplyChainTransparency.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CarbonFootprint> CarbonFootprints { get; set; }
        public DbSet<ComplianceReport> ComplianceReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RM558521"); // Use o schema do seu usuário Oracle
            base.OnModelCreating(modelBuilder);
        }
    }
}


