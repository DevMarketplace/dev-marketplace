using BusinessLogic.BusinessObjects;
using System.Collections.Generic;

namespace BusinessLogic.Services
{
    public interface ICompanyManager
    {
        /// <summary>
        /// Lists all available companies.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyBo> GetCompanies();
    }
}