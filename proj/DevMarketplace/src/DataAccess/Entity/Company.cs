using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    public class Company
    {
        [Key, Column(Order=1)]
        public Guid Id { get; set; }

        [Column(Order=2), Required, MinLength(1), MaxLength(300)]
        public string Name { get; set; }

        [Column(Order=3), DataType(DataType.Url)]
        public string Url { get; set; }

        [Column(Order = 4)]
        public string Description { get; set; }

        [Column(Order = 5), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column(Order = 6), ForeignKey("IsoCountryCode")]
        public Country Country { get; set; }

        [Column(Order=7)]
        public string Location { get; set; }
    }
}
