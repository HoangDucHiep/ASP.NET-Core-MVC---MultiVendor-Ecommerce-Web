using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }
		[ForeignKey("VendorId")]
		public int VendorId { get; set; }
		[ForeignKey("CategoryId")]
		public int CategoryId { get; set; }

		public string? Name { get; set; }

		public string? Slug { get; set; }
		public string? Description { get; set; } = string.Empty;
		[Column(TypeName = "decimal(18,2)")]
		public decimal? Price { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Sale { get; set; } = 0;
		public int? Stock { get; set; }
		public string? SKU { get; set; }

		public bool HasVariant { get; set; } = false;

		public string? Status { get; set; } = "Active";  // Active, InActive

		public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // Navigation properties
        public virtual Vendor? Vendor { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category? Category { get; set; }
		public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
		public virtual ICollection<ProductImage>? ProductImages { get; set; }

		public virtual ICollection<ProductVariantOption>? ProductVariantOptions { get; set; }
	}

}
