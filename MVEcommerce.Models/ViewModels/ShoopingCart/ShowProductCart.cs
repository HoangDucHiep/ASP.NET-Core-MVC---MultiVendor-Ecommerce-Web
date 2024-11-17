
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models.ViewModels.ShoopingCart
{
    public class ShowProductCart
    {
        public IEnumerable<CartItemViewModel> ListCart { get; set; }
        public decimal TotalPrice { get; set; }
    }
}



