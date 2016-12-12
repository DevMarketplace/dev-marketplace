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
using DataAccess.Entity;
using System;

namespace BusinessLogic.BusinessObjects
{
    public class CompanyBo
    {
        public CompanyBo()
        {

        }

        public CompanyBo(Company entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Url = entity.Url;
            Description = entity.Description;
            Email = entity.Email;
            IsoCountryCode = entity.IsoCountryCode;
            Location = entity.Location;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string IsoCountryCode { get; set; }

        public string Location { get; set; }

    }
}
