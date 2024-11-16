using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models.ViewModels.ProductDetailViewModel
{	
	public class ProductDetailViewModel
	{
		public Product product { get; set; }
		public ProductImage ProductImage { get; set; }
		public ProductVariant productVariant { get; set; }
		public ProductVariantOption productVariantOption { get; set; }

		public Category category { get; set; }
		public List<ProductImage> ProductImages { get; set; }


	}
}
