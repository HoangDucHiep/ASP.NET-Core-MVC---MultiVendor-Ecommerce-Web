using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVEcommerce.Models
{
    public class ProductVariantOption
    {
        [Key]
        public int OptionId { get; set; }

        [ForeignKey("ProductVariant")]
        public int VariantId { get; set; }

        [Required(ErrorMessage = "Valie is required")]
        public string? Value { get; set; }


        [Column(TypeName = "decimal(18,2)")]
		[Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Sale must be between 0 - 100")]
        public decimal? Sale { get; set; } = 0;

        [Range(1, double.MaxValue, ErrorMessage = "Stock must be greater than 0")]
        public int? Stock { get; set; }

        public string? Status { get; set; } = "active";  // Active, InActive
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ProductVariant? ProductVariant { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
    }
}