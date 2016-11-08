using DataAccess.Entity;

namespace BusinessLogic.BusinessObjects
{
    public class CountryBo
    {
        public CountryBo()
        {
        }

        public CountryBo(Country entity)
        {
            IsoCountryCode = entity.IsoCountryCode;
            Name = entity.Name;
        }

        public string IsoCountryCode { get; set; }

        public string Name { get; set; }
    }
}
