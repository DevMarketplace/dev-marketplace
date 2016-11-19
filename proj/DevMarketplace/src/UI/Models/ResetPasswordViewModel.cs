using System.ComponentModel.DataAnnotations;
using UI.Localization;

namespace UI.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string ProtectedId { get; set; }

        [Required]
        public string ResetToken { get; set; }

        [Required, DataType(DataType.Password), Display(Name = nameof(AccountContent.NewPasswordText), ResourceType = typeof(AccountContent))]
        [MinLength(8, ErrorMessageResourceName = nameof(AccountContent.PasswordLengthRequirementText), ErrorMessageResourceType = typeof(AccountContent)), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessageResourceType = typeof(AccountContent),
           ErrorMessageResourceName = nameof(AccountContent.PasswordRequirementsText))]

        public string NewPassword { get; set; }

        [Required]
        [Display(Name = nameof(AccountContent.RepeatPasswordText), ResourceType = typeof(AccountContent)), DataType(DataType.Password)]
        [Compare(nameof(NewPassword),ErrorMessageResourceName = nameof(AccountContent.PasswordDoesNotMatchError), ErrorMessageResourceType = typeof(AccountContent))]
        public string NewPasswordConfirm { get; set; }
    }
}
