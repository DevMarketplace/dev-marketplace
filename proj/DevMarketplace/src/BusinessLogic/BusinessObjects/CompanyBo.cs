using DataAccess.Entity;
using System;

namespace BusinessLogic.BusinessObjects
{
    public class CompanyBo
    {
        public CompanyBo()
        {

        }

        public CompanyBo(Company entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Url = entity.Url;
            Description = entity.Description;
            Email = entity.Email;
            IsoCountryCode = entity.IsoCountryCode;
            Country = new CountryBo(entity.Country);
            Location = entity.Location;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string IsoCountryCode { get; set; }

        public virtual CountryBo Country { get; set; }

        public string Location { get; set; }

    }
}
