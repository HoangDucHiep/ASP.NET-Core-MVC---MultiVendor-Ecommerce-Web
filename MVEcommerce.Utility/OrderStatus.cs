using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Utility
{
    public class OrderStatus
    {
        public const string PENDING= "Pending";
        public const string APPROVED = "Approved";
        public const string REJECTED = "Rejected";
        public const string SHIPPED = "Shipped";
        public const string CANCELLED = "Cancelled";
        public const string COMPLETED = "Completed";
    }
}
