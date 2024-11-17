using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVEcommerce.Models.ViewModels.VendorDetails
{
    public class VendorDetailVM
    {
        public Vendor Vendor { get; set; }
        public Address Address { get; set; }
    }
}