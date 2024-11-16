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
		[Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
		public string? Slug { get; set; }
		public string? Description { get; set; } = string.Empty;
		[Column(TypeName = "decimal(18,2)")]
		[Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }

		[Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Sale must be between 0 - 100")]
        public decimal? Sale { get; set; } = 0;
        [Range(1, double.MaxValue, ErrorMessage = "Stock must be greater than 0")]
        public int? Stock { get; set; }
		[Required(ErrorMessage = "SKU is required")]
        public string? SKU { get; set; }
		public bool HasVariant { get; set; } = false;

		public string? Status { get; set; } = "active";  // Active, InActive

		public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        // Navigation properties
        public virtual Vendor? Vendor { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category? Category { get; set; }
		public virtual ICollection<ProductVariant>? ProductVariants { get; set; }
		public virtual ICollection<ProductImage>? ProductImages { get; set; }
	}

}
