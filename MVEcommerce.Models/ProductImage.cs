using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVEcommerce.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductVariantOption")]
        public int? VariantOptionID { get; set; }

        [Required]
        public required string ImageUrl { get; set; }

        public bool IsMain { get; set; }

        // Navigation properties
        public virtual Product? Product { get; set; }
        public virtual ProductVariantOption? ProductVariantOption { get; set; }
    }
}