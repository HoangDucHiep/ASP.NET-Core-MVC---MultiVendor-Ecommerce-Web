using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVEcommerce.Models
{
    public class Address
    {

        public Address()
        {
        }
        public Address(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        [Key]
        public int AddressId { get; set; }

        [ForeignKey("ApplicationUser")]
        [ValidateNever]
        public string UserId { get; set; }

        [ForeignKey("Vendor")]
        [ValidateNever]
        public int? VendorId { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;
        [Required(ErrorMessage = "Apartment is required")]
        public string Apartment { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
