#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.BusinessObjects;
using DataAccess.Entity;
using DataAccess.Repository;

namespace BusinessLogic.Managers
{
    public class CompanyManager : ICompanyManager
    {
        private const string DefaultCompanyEmail = "noemail@devmarketplace.com";
        private readonly IGenericRepository<Company> _companyRepository;
        private readonly IGenericRepository<CompanyAdmin> _companyAdminRepository;

        public CompanyManager(IGenericRepository<Company> companyRepository, IGenericRepository<CompanyAdmin> companyAdminRepository)
        {
            _companyRepository = companyRepository;
            _companyAdminRepository = companyAdminRepository;
        }

        public string GetDefaultCompanyEmail => DefaultCompanyEmail;

        public IEnumerable<CompanyBo> GetCompanies()
        {
            return _companyRepository.Get().Select(x => new CompanyBo(x));
        }

        public CompanyBo Get(Guid id)
        {
            return new CompanyBo(_companyRepository.GetByID(id));
        }

        public CompanyBo GetByEmail(string companyEmail)
        {
            var company = _companyRepository.Get(x => x.Email == companyEmail).FirstOrDefault();
            return company != null ? new CompanyBo(company) : null;
        }

        public Guid Create(CompanyBo company)
        {
            var entity = company.ToEntity<Company>();
            _companyRepository.Insert(entity);
            _companyRepository.SubmitChanges();
            if (entity.Id != null)
            {
                return entity.Id.Value;
            }

            return Guid.Empty;
        }

        public void Update(CompanyBo company)
        {
            _companyRepository.Update(company.ToEntity<Company>());
            _companyRepository.SubmitChanges();
        }

        public void SetAdmin(Guid userId, Guid companyId)
        {
            var company = _companyRepository.GetByID(companyId);
            if (company.Email == DefaultCompanyEmail)
            {
                return;
            }

            var companyAdmin = _companyAdminRepository.Get(ca => ca.CompanyId == companyId).FirstOrDefault();
            if(companyAdmin == null)
            {
                _companyAdminRepository.Insert(new CompanyAdmin { CompanyId = companyId, UserId = userId.ToString() });
                _companyAdminRepository.SubmitChanges();
            }
        }

        public bool IsUserCompanyAdmin(string userId, Guid companyId)
        {
            return _companyAdminRepository.Get(ca => ca.CompanyId == companyId && ca.UserId == userId.ToString()).Any();
        }
    }
}
