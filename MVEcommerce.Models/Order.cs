using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVEcommerce.Models
{
    public class Order
    {
        public Order()
        {
            OrderId = Guid.NewGuid();
            SubOrders = new HashSet<Order>();
        }

        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Vendor")]
        public int? VendorId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled

        [ForeignKey("ParentOrder")]
        public Guid? ParentOrderId { get; set; }
        public virtual Order ParentOrder { get; set; }

        public virtual ICollection<Order> SubOrders { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public string ShippingAddress { get; set; }
    }
}