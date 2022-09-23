using System.ComponentModel.DataAnnotations;

namespace Coop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name  { get; set; }
        [Required]
        [StringLength(20)]
        public string EANCode { get; set; }

        public IList<AssortmentProduct> AssortmentProducts { get; set; }

    }

 
}
