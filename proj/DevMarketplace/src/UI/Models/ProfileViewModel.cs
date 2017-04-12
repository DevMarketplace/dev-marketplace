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

using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Mvc;
using UI.Localization;

namespace UI.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            //Default constructor
        }

        public ProfileViewModel(ProfileViewModel model, ICompanyManager companyManager)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            CompanyId = model.CompanyId;
            Companies = TransformCompanies(companyManager);
            IsCompanyAdmin = model.IsCompanyAdmin;
        }

        public ProfileViewModel(ApplicationUser user, ICompanyManager companyManager)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            CompanyId = user.CompanyId;
            Companies = TransformCompanies(companyManager);
            IsCompanyAdmin = companyManager.IsUserCompanyAdmin(user.Id, user.CompanyId);
        }

        public static void SetUserProperties(ProfileViewModel model, ApplicationUser user)
        {
            user.UserName = model.Email;
            user.Email = model.Email;
            user.CompanyId = model.CompanyId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
        }

        private List<SelectListItem> TransformCompanies(ICompanyManager manager)
        {
            return manager.GetCompanies().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        }

        [Required, DataType(DataType.Text), Display(ResourceType = typeof(AccountContent), Name = nameof(AccountContent.FirstNameText))]
        public string FirstName { get; set; }

        [Required, DataType(DataType.Text), Display(ResourceType = typeof(AccountContent), Name = nameof(AccountContent.LastNameText))]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress), Display(Name = nameof(AccountContent.EmailText), ResourceType = typeof(AccountContent))]
        public string Email { get; set; }

        [Required, Display(Name = nameof(AccountContent.CompanyText), ResourceType = typeof(AccountContent))]
        public Guid CompanyId { get; set; }

        public List<SelectListItem> Companies { get; set; }

        [Required]
        public bool IsCompanyAdmin { get; set; }
    }
}
