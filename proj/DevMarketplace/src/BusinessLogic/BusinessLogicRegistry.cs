using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BusinessLogicRegistry : Registry
    {
        public BusinessLogicRegistry()
        {
            Scan(scan => 
            {
                scan.AssemblyContainingType<BusinessLogicRegistry>();
                scan.WithDefaultConventions();
            });
        }
    }
}
