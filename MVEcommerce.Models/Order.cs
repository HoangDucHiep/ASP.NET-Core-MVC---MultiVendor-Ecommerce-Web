// Order.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVEcommerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled

        [ForeignKey("ParentOrder")]
        public int? ParentOrderId { get; set; }
        public virtual Order ParentOrder { get; set; }

        public virtual ICollection<Order> SubOrders { get; set; } = new HashSet<Order>();

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
    }
}