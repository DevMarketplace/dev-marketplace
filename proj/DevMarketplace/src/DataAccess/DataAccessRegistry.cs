using StructureMap;
using DataAccess.Abstractions;
using DataAccess.Repository;
using Microsoft.AspNetCore.Identity;

namespace DataAccess
{
    public class DataAccessRegistry : Registry
    {
        public DataAccessRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType<DataAccessRegistry>();
                scan.WithDefaultConventions();
                scan.AddAllTypesOf(typeof(IGenericRepository<>));
            });

            For<IDataContext>().Use<DevMarketplaceDataContext>();
        }
    }
}
