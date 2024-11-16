using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var orders = _unitOfWork.Order.GetAll(
                o => o.UserId == claim.Value,
                includeProperties: "OrderDetails,OrderDetails.Product"
            );

            return View(orders);
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            // Get the shopping cart items for the user
            var cartItems = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == userId, includeProperties: "Product").ToList();

            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    // Group cart items by vendor
                    var vendorGroups = cartItems.GroupBy(c => c.Product.VendorId);

                    // Create the main order
                    var mainOrder = new Order
                    {
                        UserId = userId,
                        OrderDate = DateTime.Now,
                        Status = "Pending",
                        TotalAmount = 0 // Will be updated later
                    };
                    _unitOfWork.Order.Add(mainOrder);
                    _unitOfWork.Save();

                    // Create suborders for each vendor group
                    foreach (var vendorGroup in vendorGroups)
                    {
                        var subOrder = new Order
                        {
                            UserId = userId,
                            OrderDate = DateTime.Now,
                            Status = "Pending",
                            ParentOrderId = mainOrder.OrderId,
                            TotalAmount = 0 // Will be updated later
                        };
                        _unitOfWork.Order.Add(subOrder);
                        _unitOfWork.Save();

                        // Create order details for each product in the vendor group
                        foreach (var cartItem in vendorGroup)
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderId = subOrder.OrderId,
                                ProductId = cartItem.ProductId,
                                Quantity = cartItem.Quantity,
                                Price = cartItem.Product.Price.Value
                            };
                            _unitOfWork.OrderDetail.Add(orderDetail);

                            // Update suborder total amount
                            subOrder.TotalAmount += orderDetail.Price * orderDetail.Quantity;
                        }

                        // Update main order total amount
                        mainOrder.TotalAmount += subOrder.TotalAmount;
                    }

                    // Save all changes
                    _unitOfWork.Save();

                    // Clear the shopping cart
                    _unitOfWork.ShoppingCart.RemoveRange(cartItems);
                    _unitOfWork.Save();

                    // Commit the transaction
                    transaction.Commit();

                    return RedirectToAction("OrderConfirmation");
                }
                catch (Exception)
                {
                    // Rollback the transaction if any error occurs
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IActionResult PlaceOrderTest()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var cartItems = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == userId, includeProperties: "Product").ToList();

            return View(cartItems);
        }
    }
}