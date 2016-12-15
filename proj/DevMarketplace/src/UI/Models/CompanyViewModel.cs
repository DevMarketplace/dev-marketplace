using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class CompanyViewModel
    {
        [Required, MinLength(1), MaxLength(300)]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string IsoCountryCode { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public string Location { get; set; }
    }
}
