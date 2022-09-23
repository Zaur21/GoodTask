using Microsoft.EntityFrameworkCore;

namespace Coop.Models
{
  
    public class AssortmentProduct
    {
        public int AssortmentId { get; set; }
        public Assortment Assortments { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

    }
}
