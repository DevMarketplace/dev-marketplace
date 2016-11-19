using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess
{
    /// <summary>
    /// This context factory only exists because EF Core doesn't support migrations from class libraries.
    /// Please remove when a better version is released.
    /// </summary>
    internal class DevMarketplaceDataContextFactory : IDbContextFactory<DevMarketplaceDataContext>
    {
        public DevMarketplaceDataContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DevMarketplaceDataContext>();
            builder.UseSqlServer("Server=.;Database=DevMarketplace;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new DevMarketplaceDataContext(builder.Options);
        }
    }
}
