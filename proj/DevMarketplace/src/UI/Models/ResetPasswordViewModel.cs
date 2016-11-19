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

        [Required]
        [Display(Name = nameof(AccountContent.NewPasswordText), ResourceType = typeof(AccountContent)), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = nameof(AccountContent.RepeatPasswordText), ResourceType = typeof(AccountContent)), DataType(DataType.Password)]
        [Compare(nameof(NewPassword),ErrorMessageResourceName = nameof(AccountContent.PasswordDoesNotMatchError), ErrorMessageResourceType = typeof(AccountContent))]
        public string NewPasswordConfirm { get; set; }
    }
}
