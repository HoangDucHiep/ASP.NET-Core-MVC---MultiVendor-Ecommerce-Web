using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models
{
    public class Vendor
    {
        [Key]
        public int VendorId { get; set; }


        public string? Name { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation property
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
