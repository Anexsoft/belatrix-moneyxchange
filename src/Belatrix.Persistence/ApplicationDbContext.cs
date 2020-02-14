using Belatrix.Models.Domain;
using Belatrix.Persistence.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Belatrix.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        )
            : base(options)
        {
            
        }

        public DbSet<Currency> Currencies { get; set;}
        public DbSet<CurrencyAudit> Audit { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new CurrencyConfiguration(modelBuilder.Entity<Currency>());
            new CurrencyAuditConfiguration(modelBuilder.Entity<CurrencyAudit>());
        }
    }
}
