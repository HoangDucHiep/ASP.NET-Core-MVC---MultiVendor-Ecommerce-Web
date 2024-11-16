using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVEcommerce.Models
{
    public class ProductVariant
    {
        [Key]
        public int VariantId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string? Name { get; set; }
        public string? Status { get; set; } = "active";  // Active, InActive
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Product? Product { get; set; }
        public virtual ICollection<ProductVariantOption>? ProductVariantOptions { get; set; }
    }
}
