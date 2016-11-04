using StructureMap;
using DataAccess.Abstractions;
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
            });
        }
    }
}
