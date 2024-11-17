using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVEcommerce.Models.ViewModels.VendorProduct
{
    public class VendorAddProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? Categories { get; set; }
        public ProductVariant ProductVariant { get; set; } = new ProductVariant();
        public List<ProductVariantOptionVM> ProductVariantOptions { get; set; } = new List<ProductVariantOptionVM>();
    }

    public class ProductVariantOptionVM : ProductVariantOption
    {
        public List<IFormFile>? OptionImages { get; set; }
    }
}