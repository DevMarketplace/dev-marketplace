using System.ComponentModel.DataAnnotations;
using UI.Localization;

namespace UI.Models
{
    public class ForgotPasswordViewModel
    {
        [Required, Display(Name = nameof(AccountContent.EmailText), ResourceType = typeof(AccountContent)), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
