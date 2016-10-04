using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Country
    {
        [Key, StringLength(2), Column(Order=1)]
        public string IsoCountryCode { get; set; }

        [Column(Order=2)]
        public string Name {get; set;}
    }
}
