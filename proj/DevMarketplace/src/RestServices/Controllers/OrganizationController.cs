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
using System.Net;
using BusinessLogic.BusinessObjects;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Mvc;
using RestServices.Messages.Request;
using RestServices.Messages.Response;

namespace RestServices.Controllers
{
    [Route("api/v1/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly ICompanyManager _companyManager;

        public OrganizationController(ICompanyManager companyManager)
        {
            _companyManager = companyManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(new GenericResponseMessage<IEnumerable<CompanyBo>>(_companyManager.GetCompanies()));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var organization = _companyManager.Get(id);
                return new OkObjectResult(new GenericResponseMessage<CompanyBo>(organization));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new GenericResponseMessage<CompanyBo>
                    {
                        Errors = new List<string> {ex.Message},
                        StatusCode = HttpStatusCode.BadRequest
                    });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrganizationRequestMessage organization)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var key in ModelState.Keys)
                {
                    if (ModelState[key].Errors.Any())
                    {
                        errors.Add($"{key}: {string.Join(",", ModelState[key].Errors.Select(x => x.ErrorMessage))}");
                    }
                }
                return new BadRequestObjectResult(new GenericResponseMessage<CompanyBo>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = errors
                });
            }

            var company = _companyManager.GetByName(organization.Name);
            if (company != null)
            {
                return new BadRequestObjectResult(new GenericResponseMessage<CompanyBo>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new[] {"The company already exists"}
                    });
            }

            Guid companyId;
            try
            {
                company = organization.ToBusinessObject<CompanyBo>();
                companyId = _companyManager.Create(company);
            }
            catch (Exception)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            return new OkObjectResult(new { companyId, Name = company.Name });
        }
    }
}
