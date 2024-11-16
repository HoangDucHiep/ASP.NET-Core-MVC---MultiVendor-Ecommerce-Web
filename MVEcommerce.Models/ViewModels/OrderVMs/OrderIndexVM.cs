namespace MVEcommerce.Models.ViewModels.OrderVMs
{
    public class OrderIndexViewModel
    {
        public ApplicationUser? User { get; set; }
        public List<Order>? Orders { get; set; }
        public Address? address { get; set; }
    }
}