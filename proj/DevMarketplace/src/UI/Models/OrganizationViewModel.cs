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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UI.Localization;

namespace UI.Models
{
    public class OrganizationViewModel
    {
        [Required, MinLength(1), MaxLength(300), Display(ResourceType = typeof(OrganizationContent), Name = nameof(OrganizationContent.OrganizationNameText))]
        public string Name { get; set; }

        [DataType(DataType.Url), Display(ResourceType = typeof(OrganizationContent), Name = nameof(OrganizationContent.WebSiteText))]
        public string Url { get; set; }

        [Required, DataType(DataType.MultilineText), Display(ResourceType = typeof(OrganizationContent), Name = nameof(OrganizationContent.OrganizationDescription))]
        public string Description { get; set; }

        [Required, DataType(DataType.EmailAddress), Display(ResourceType = typeof(OrganizationContent), Name = nameof(OrganizationContent.OrganizationEmailText))]
        public string Email { get; set; }

        [Required, Display(ResourceType = typeof(OrganizationContent), Name = nameof(OrganizationContent.CountryText))]
        public string IsoCountryCode { get; set; }

        public List<SelectListItem> Countries { get; set; }

        [DataType(DataType.Text), Display(ResourceType = typeof(OrganizationContent), Name = nameof(OrganizationContent.LocationText))]
        public string Location { get; set; }
    }
}
