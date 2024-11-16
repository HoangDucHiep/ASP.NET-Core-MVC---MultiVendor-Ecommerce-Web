using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models
{
    public class Address
    {
        public Address(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        [Key]
        public int AddressId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ForeignKey("Vendor")]
        public int? VendorId { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Apartment { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
