using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity
{
    public class CardStatus
    {
        [Key]
        public string Status { get; set; }
    }
}
