using System.ComponentModel.DataAnnotations;
using UI.Localization;

namespace UI.Models
{
    public class SignInViewModel
    {
        /// <summary>
        /// Optional return URL
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// The email serves as a username
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(AccountContent), Name = "EmailText")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [Display(ResourceType = typeof(AccountContent), Name = "PasswordText")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
