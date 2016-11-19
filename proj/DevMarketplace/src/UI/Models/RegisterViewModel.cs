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
