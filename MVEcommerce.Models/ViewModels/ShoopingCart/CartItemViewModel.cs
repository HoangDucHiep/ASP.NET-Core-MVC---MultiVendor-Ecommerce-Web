using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models.ViewModels.ShoopingCart
{
	public class CartItemViewModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string VariantName { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
		public string ImageUrl { get; set; }
		public int Stock { get; set; }
		public string VendorName { get; set; } 
		public int CartId { get; set; }
	}
}
