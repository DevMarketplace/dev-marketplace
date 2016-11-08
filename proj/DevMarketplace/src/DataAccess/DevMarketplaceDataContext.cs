using DataAccess.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    /// <summary>
    /// The main DataContext for the developer marketplace
    /// </summary>
    public sealed class DevMarketplaceDataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DbSet<Country> Country { get; set; }

        public DbSet<Company> Company { get; set; }

        public DevMarketplaceDataContext()
        {
            Database.EnsureCreated();
        }

        public DevMarketplaceDataContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Company>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");
        }

        void IDataContext.SaveChanges()
        {
            SaveChanges();
        }
    }
}
