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
		[Required]
		public required string Name { get; set; }
		[Required]
		public required string Slug { get; set; }
		public string? Description { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal? Price { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Sale { get; set; }
		public int? Stock { get; set; }
		public string? SKU { get; set; }
		[Required]
		public bool HasVariant { get; set; }
		[Required]
		public required string Status { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		[Required]
		public DateTime UpdatedAt { get; set; }
		// Navigation properties
		public virtual Vendor? Vendor { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category? Category { get; set; }
		public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
		public virtual ICollection<ProductImage>? ProductImages { get; set; }
	}

}
