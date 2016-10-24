using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
