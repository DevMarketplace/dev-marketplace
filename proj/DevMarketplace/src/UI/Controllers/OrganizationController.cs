﻿#region License
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
using System.Linq;
using BusinessLogic.Managers;
using DataAccess.Entity;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.Models;

namespace UI.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly ICountryManager _countryManager;
        private readonly ICompanyManager _companyManager;

        public OrganizationController(ICountryManager countryManager, ICompanyManager companyManager)
        {
            _countryManager = countryManager;
            _companyManager = companyManager;
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var model = new OrganizationViewModel();
            model.Countries = _countryManager.GetCountries().Select(c => new SelectListItem { Text = c.Name, Value = c.IsoCountryCode }).ToList();
            return View(nameof(Update), model);
        }

        [HttpPost]
        public IActionResult Update(OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = _countryManager.GetCountries().Select(c => new SelectListItem { Text = c.Name, Value = c.IsoCountryCode }).ToList();
                return View(nameof(Update), model);
            }

            return View(nameof(Update), model);
        }
    }
}
