using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using System.Linq;
using System.Security.Claims;

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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult PlaceOrder()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var cartItems = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == userId, includeProperties: "Product");

        //    if (cartItems == null || !cartItems.Any())
        //    {
        //        ModelState.AddModelError("", "Your cart is empty.");
        //        return RedirectToAction("Index", "ShoppingCart");
        //    }

        //    var groupedCartItems = cartItems.GroupBy(c => c.Product.VendorId);

        //    using (var transaction = _unitOfWork.BeginTransaction())
        //    {
        //        try
        //        {
        //            var order = new Order
        //            {
        //                UserId = userId,
        //                Status = "Pending",
        //                OrderDate = DateTime.Now
        //            };

        //            _unitOfWork.Order.Add(order);
        //            _unitOfWork.Save();

        //            foreach (var group in groupedCartItems)
        //            {
        //                var subOrder = new SubOrder
        //                {
        //                    OrderId = order.OrderId,
        //                    VendorId = group.Key,
        //                    Status = "Pending",
        //                    OrderDate = DateTime.Now
        //                };

        //                _unitOfWork.SubOrder.Add(subOrder);
        //                _unitOfWork.Save();

        //                foreach (var cartItem in group)
        //                {
        //                    var orderDetail = new OrderDetail
        //                    {
        //                        SubOrderId = subOrder.SubOrderId,
        //                        ProductId = cartItem.ProductId,
        //                        Quantity = cartItem.Quantity,
        //                        Price = cartItem.Product.Price ?? 0
        //                    };

        //                    _unitOfWork.OrderDetail.Add(orderDetail);
        //                }

        //                _unitOfWork.Save();
        //            }

        //            _unitOfWork.ShoppingCart.RemoveRange(cartItems);
        //            _unitOfWork.Save();

        //            transaction.Commit();
        //            return RedirectToAction("OrderConfirmation");
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            ModelState.AddModelError("", "An error occurred while placing the order. Please try again.");
        //            return RedirectToAction("Index", "ShoppingCart");
        //        }
        //    }
        //}

        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}