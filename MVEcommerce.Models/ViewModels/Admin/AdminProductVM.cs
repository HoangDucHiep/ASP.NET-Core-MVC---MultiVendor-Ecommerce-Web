using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models.ViewModels.Admin
{

    public class AdminProductVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductVariantOption> Options { get; set; }
    }

}
