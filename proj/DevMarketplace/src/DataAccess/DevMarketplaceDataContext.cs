using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DevMarketplaceDataContext : DbContext, IDataContext
    {
        public DbSet<Country> Country { get; set; }

        public DevMarketplaceDataContext()
        {

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
