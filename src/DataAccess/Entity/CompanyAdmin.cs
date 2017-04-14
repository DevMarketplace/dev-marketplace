using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    public class CompanyAdmin
    {
        [Key, Column(Order=1)]
        public string UserId {get; set; }

        [Key, Column(Order=2)]
        public Guid? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
