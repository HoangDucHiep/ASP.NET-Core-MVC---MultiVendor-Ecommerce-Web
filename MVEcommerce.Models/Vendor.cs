﻿using System;
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

        public string? Status { get; set; } = "Active";  // Active, InActive

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
