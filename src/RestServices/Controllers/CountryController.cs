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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BusinessLogic.Managers;
using System.Threading.Tasks;
using BusinessLogic.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using RestServices.Messages.Response;

namespace RestServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryManager _countryManager;

        public CountryController(ICountryManager countryManager)
        {
            _countryManager = countryManager;
        }

        [HttpGet("{countryCode}")]
        public async Task<IActionResult> Get(string countryCode)
        {
            try
            {
                return await Task.Run<IActionResult>(() => new OkObjectResult(new GenericResponseMessage<CountryBo>(_countryManager.Get(countryCode))));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new BadRequestObjectResult(new GenericResponseMessage<CompanyBo>
                    {
                        Errors = new List<string> { ex.Message },
                        StatusCode = HttpStatusCode.BadRequest
                    }));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run<IActionResult>(() => new OkObjectResult(new GenericResponseMessage<IEnumerable<CountryBo>>(_countryManager.GetCountries()
                .OrderByDescending(c => c.IsoCountryCode == "US")
                .ThenBy(c => c.Name))));
        }
    }
}
