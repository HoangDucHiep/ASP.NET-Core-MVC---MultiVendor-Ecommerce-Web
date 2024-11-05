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

        [Required]
        public required string Value { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public required string SKU { get; set; }

        [Required]
        public required string Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual ProductVariant? ProductVariant { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
    }
}