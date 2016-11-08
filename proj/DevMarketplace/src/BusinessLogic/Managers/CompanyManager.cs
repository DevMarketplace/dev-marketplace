using BusinessLogic.BusinessObjects;
using DataAccess.Entity;
using DataAccess.Repository;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class CompanyManager : ICompanyManager
    {
        private readonly IGenericRepository<Company> _companyRepository;

        public CompanyManager(IGenericRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IEnumerable<CompanyBo> GetCompanies()
        {
            return _companyRepository.Get().Select(x => new CompanyBo(x));
        }
    }
}
