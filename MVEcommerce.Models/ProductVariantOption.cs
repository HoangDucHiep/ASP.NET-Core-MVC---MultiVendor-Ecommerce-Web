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


        public string Value { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
		public decimal? Sale { get; set; }


        public int Stock { get; set; }


        public string SKU { get; set; }


        public string Status { get; set; }


        public DateTime CreatedAt { get; set; }


        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public virtual ProductVariant? ProductVariant { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
    }
}