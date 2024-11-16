using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.OrderVMs;
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
        private readonly ApplicationDbContext _db;

        public OrderController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            
            if (User.Identity is not ClaimsIdentity claimsIdentity)
            {
                return RedirectToAction("Error", "Home");
            }
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var userId = userIdClaim.Value;

            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId)!;

            var orders = _unitOfWork.Order.GetAll(o => o.UserId == userId, includeProperties: "OrderDetails,OrderDetails.Product.Vendor").OrderByDescending(o => o.OrderDate).ToList();

            Address userAddress = _unitOfWork.Address.Get(a=>a.UserId == userId);

            var vm = new OrderIndexViewModel
            {
                User = user,
                Orders = orders,
                address = userAddress
            };

            return View(vm);
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
                    var vendorGroups = cartItems.GroupBy(c => c.Product.VendorId).ToList();

                    // Check if all products are from the same vendor
                    if (vendorGroups.Count == 1)
                    {
                        // Create the main order
                        var mainOrder = CreateOrder(userId, null);
                        _unitOfWork.Order.Add(mainOrder);
                        _unitOfWork.Save();

                        // Create order details for each product
                        foreach (var cartItem in cartItems)
                        {
                            var orderDetail = CreateOrderDetail(mainOrder.OrderId, cartItem);
                            _unitOfWork.OrderDetail.Add(orderDetail);

                            // Update main order total amount
                            mainOrder.TotalAmount += orderDetail.Price * orderDetail.Quantity;
                        }

                        // Save all changes
                        _unitOfWork.Save();
                    }
                    else
                    {
                        // Create the main order
                        var mainOrder = CreateOrder(userId, null);
                        _unitOfWork.Order.Add(mainOrder);
                        _unitOfWork.Save();

                        // Create suborders for each vendor group
                        foreach (var vendorGroup in vendorGroups)
                        {
                            var subOrder = CreateOrder(userId, mainOrder.OrderId);
                            _unitOfWork.Order.Add(subOrder);
                            _unitOfWork.Save();

                            foreach (var cartItem in vendorGroup)
                            {
                                var orderDetail = CreateOrderDetail(subOrder.OrderId, cartItem);
                                _unitOfWork.OrderDetail.Add(orderDetail);

                                // Update suborder total amount
                                subOrder.TotalAmount += orderDetail.Price * orderDetail.Quantity;
                            }

                            mainOrder.TotalAmount += subOrder.TotalAmount;
                        }

                        _unitOfWork.Save();
                    }

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

        private Order CreateOrder(string userId, Guid? parentOrderId)
        {
            return new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                ParentOrderId = parentOrderId,
                TotalAmount = 0 // Will be updated later
            };
        }

        private OrderDetail CreateOrderDetail(Guid orderId, ShoppingCart cartItem)
        {
            return new OrderDetail
            {
                OrderId = orderId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Product.Price.Value
            };
        }
    }
}