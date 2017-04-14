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
using System.Linq;
using BusinessLogic.BusinessObjects;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using UI.Models;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace UI.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly ICountryManager _countryManager;
        private readonly ICompanyManager _companyManager;
        private readonly ILogger _logger;

        public OrganizationController(ICountryManager countryManager, ICompanyManager companyManager, ILoggerFactory loggerFactory)
        {
            _countryManager = countryManager;
            _companyManager = companyManager;
            _logger = loggerFactory.CreateLogger(nameof(OrganizationController));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update(Guid id)
        {
            try
            {
                var company = _companyManager.Get(id);
                if (company == null)
                {
                    return NotFound();
                }

                var model = new OrganizationViewModel(company)
                {
                    Countries = _countryManager.GetCountries()
                        .Select(c => new SelectListItem
                        {
                            Text = c.Name,
                            Value = c.IsoCountryCode
                        }).ToList()
                };

                return View(nameof(Update), model);

            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message, e);
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(OrganizationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = _countryManager.GetCountries().Select(c => new SelectListItem { Text = c.Name, Value = c.IsoCountryCode }).ToList();
                return View(nameof(Update), model);
            }

            try
            {
                var company = model.ToBusinessObject<CompanyBo>();
                _companyManager.Update(company);

                return RedirectToAction(nameof(Update), model.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return View(nameof(Update), model);
            }
        }
    }
}
