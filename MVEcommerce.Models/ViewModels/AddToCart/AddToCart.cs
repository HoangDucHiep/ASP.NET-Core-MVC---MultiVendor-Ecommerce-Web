using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models.ViewModels.AddToCart
{
    public class AddToCart
    {
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public int? VariantOptionID { get; set; }
	}
}
