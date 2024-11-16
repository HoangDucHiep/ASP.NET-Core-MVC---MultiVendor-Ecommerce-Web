using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVEcommerce.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ValidateNever]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductVariantOption")]
        public int? VariantOptionID { get; set; } = null;
        public virtual ProductVariantOption? ProductVariantOption { get; set; }

        [ValidateNever]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; } // Thêm thuộc tính Quantity
    }
}