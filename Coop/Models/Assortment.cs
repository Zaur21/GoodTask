using System.ComponentModel.DataAnnotations;

namespace Coop.Models
{
    public class Assortment
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }

        public IList<AssortmentProduct> AssortmentProducts { get; set; }


    }
}
