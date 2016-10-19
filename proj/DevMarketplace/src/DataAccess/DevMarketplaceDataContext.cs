using DataAccess.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DevMarketplaceDataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DbSet<Country> Country { get; set; }

        public DevMarketplaceDataContext()
        {
            Database.EnsureCreated();
        }

        public DevMarketplaceDataContext(DbContextOptions options) : base(options)
        {
            
        }

        void IDataContext.SaveChanges()
        {
            SaveChanges();
        }
    }
}
