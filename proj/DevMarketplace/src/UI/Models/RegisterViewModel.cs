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
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Localization;

namespace UI.Models
{
    public class RegisterViewModel
    {
        [Required, DataType(DataType.Text), Display(ResourceType = typeof(AccountContent), Name = nameof(AccountContent.FirstNameText))]
        public string FirstName { get; set; }

        [Required, DataType(DataType.Text), Display(ResourceType = typeof(AccountContent), Name = nameof(AccountContent.LastNameText))]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress), Display(Name = nameof(AccountContent.EmailText), ResourceType = typeof(AccountContent))]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = nameof(AccountContent.PasswordText), ResourceType = typeof(AccountContent))]
        [MinLength(8, ErrorMessageResourceName = nameof(AccountContent.PasswordLengthRequirementText), ErrorMessageResourceType = typeof(AccountContent)), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessageResourceType = typeof(AccountContent), 
           ErrorMessageResourceName = nameof(AccountContent.PasswordRequirementsText))]
        public string Password { get; set; }

        [Required, Display(Name=nameof(AccountContent.CompanyText), ResourceType= typeof(AccountContent))]
        public Guid CompanyId { get; set; }

        [Required, DataType(DataType.Password), Display(Name = nameof(AccountContent.RepeatPasswordText), ResourceType = typeof(AccountContent))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(AccountContent.PasswordDoesNotMatchError), ErrorMessageResourceType = typeof(AccountContent))]
        public string PasswordConfirm { get; set; }

        public List<SelectListItem> Companies { get; set; }
    }
}
