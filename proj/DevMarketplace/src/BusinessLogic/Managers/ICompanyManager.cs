#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2017 Tosho Toshev
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
using BusinessLogic.BusinessObjects;

namespace BusinessLogic.Managers
{
    public interface ICompanyManager
    {
        /// <summary>
        /// Contains the default e-mail intended when the 'No Company' option is selected
        /// </summary>
        string GetDefaultCompanyEmail { get; }

        /// <summary>
        /// Lists all available organizations.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyBo> GetCompanies();

        /// <summary>
        /// Gets an organization by its Id
        /// </summary>
        /// <param name="id">An organization ID Guid</param>
        /// <returns></returns>
        CompanyBo Get(Guid id);

        /// <summary>
        /// Gets a company by name
        /// </summary>
        /// <param name="companyEmail"></param>
        /// <returns></returns>
        CompanyBo GetByEmail(string companyEmail);

        /// <summary>
        /// Creates a new company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        Guid Create(CompanyBo company);

        /// <summary>
        /// Updates an existing company record
        /// </summary>
        /// <param name="company"></param>
        void Update(CompanyBo company);

        /// <summary>
        /// Sets a user as a company admin
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="companyId"></param>
        void SetAdmin(Guid userId, Guid companyId);

        /// <summary>
        /// Checks if user is a company admin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        bool IsUserCompanyAdmin(string userId, Guid companyId);
    }
}