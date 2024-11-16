using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVEcommerce.Models
{
    public class OrderDetail
    {
        public OrderDetail()
        {
            OrderDetailId = Guid.NewGuid();
        }

        [Key]
        public Guid OrderDetailId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}