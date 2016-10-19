using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Company
    {
        [Key, Column(Order=1)]
        public Guid Id { get; set; }

        [Column(Order=2), Required, MinLength(1), MaxLength(300)]
        public string Name { get; set; }
    }
}
