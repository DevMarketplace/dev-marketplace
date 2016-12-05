using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        }

        public ProfileViewModel(ApplicationUser user, ICompanyManager companyManager)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            CompanyId = user.CompanyId;
            Companies = TransformCompanies(companyManager);
        }

        public static ApplicationUser GetUser(ProfileViewModel model)
        {
            return new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                CompanyId = model.CompanyId,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
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
    }
}
