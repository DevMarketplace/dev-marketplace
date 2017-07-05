﻿#region License
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    public class Project : IHasIdentityEntity
    {
        [Key, Column(Order = 1)]
        public Guid? Id { get; set; }

        [Required, Column(Order = 2)]
        public string Title { get; set; }

        [Required, Column(Order = 3)]
        public string Summary { get; set; }

        [Required, Column(Order = 4)]
        public string Description { get; set; }

        [Required, Column(Order = 5)]
        public string TileImageUrl { get; set; }

        [Column(Order = 6)]
        public string MainImageUrl { get; set; }

        [Column(Order = 7)]
        public string VanityImageUrl { get; set; }

        [Required, Column(Order = 8)]
        public string Status { get; set; }

        [Column(Order = 9)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 10)]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("Status")]
        public virtual ProjectStatus ProjectStatus {get; set;}

        public ICollection<ProjectOwner> ProjectOwners { get; set; }

        public ICollection<Card> ProjectCards { get; set; }
}
}