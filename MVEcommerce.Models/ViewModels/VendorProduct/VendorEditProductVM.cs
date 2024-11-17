using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVEcommerce.Models.ViewModels.VendorProduct
{
    public class VendorEditProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        public ProductVariant ProductVariant { get; set; } = new ProductVariant();
        public List<ProductVariantOptionVM> ProductVariantOptions { get; set; } = new List<ProductVariantOptionVM>();
        public List<ProductVariantOptionVM> NewProductVariantOptions { get; set; } = new();


    }
}