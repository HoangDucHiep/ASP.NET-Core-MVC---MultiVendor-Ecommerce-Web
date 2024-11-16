namespace MVEcommerce.Models.ViewModels.OrderVMs
{
    public class OrderIndexViewModel
    {
        public ApplicationUser? User { get; set; }
        public List<ShoppingCart>? Carts { get; set; }
        public Address? Address { get; set; }
    }
}