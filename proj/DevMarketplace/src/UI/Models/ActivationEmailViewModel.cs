using DataAccess;

namespace UI.Models
{
    public class ActivationEmailViewModel
    {
        public ApplicationUser User { get; set; }

        public string ActivationUrl { get; set; }
    }
}
