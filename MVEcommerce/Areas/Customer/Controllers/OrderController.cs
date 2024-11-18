using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.OrderVMs;
using MVEcommerce.Utility;
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

        [Authorize]
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
        
            var carts = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == userId, includeProperties: "Product.ProductImages,Product.Vendor,ProductVariantOption").ToList();
        
            Address userAddress = _unitOfWork.Address.Get(a => a.UserId == userId);
        
            if (userAddress == null)
            {
                userAddress = new Address(user.Email, user.PhoneNumber);
            }
        
            var vm = new OrderIndexViewModel
            {
                User = user,
                Carts = carts,
                Address = userAddress
            };
        
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(Address address)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            // Get the shopping cart items for the user
            var cartItems = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == userId, includeProperties: "Product.ProductImages,Product.Vendor,ProductVariantOption").ToList();

            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    // Update user address
                    var userAddress = _unitOfWork.Address.Get(a => a.UserId == userId && a.AddressId == address.AddressId);

                    if (userAddress == null)
                    {
                        address.UserId = userId;
                        _unitOfWork.Address.Add(address);
                    }
                    else
                    {
                        userAddress.Country = address.Country;
                        userAddress.City = address.City;
                        userAddress.Street = address.Street;
                        userAddress.Apartment = address.Apartment;
                        userAddress.ZipCode = address.ZipCode;
                        userAddress.Email = address.Email;
                        userAddress.PhoneNumber = address.PhoneNumber;

                        _unitOfWork.Address.Update(userAddress);
                    }

                    _unitOfWork.Save();

                    string shippingAddress = address.Country + ", " + address.City + ", " + address.Street + ", " + address.Apartment;

                    // Group cart items by vendor
                    var vendorGroups = cartItems.GroupBy(c => c.Product.VendorId).ToList();

                    // Check if all products are from the same vendor
                    if (vendorGroups.Count == 1)
                    {
                        // Create the main order
                        var mainOrder = CreateOrder(userId, null, shippingAddress);
                        
                        mainOrder.VendorId = vendorGroups[0].Key;

                        _unitOfWork.Order.Add(mainOrder);
                        _unitOfWork.Save();

                        // Create order details for each product
                        foreach (var cartItem in cartItems)
                        {
                            var orderDetail = CreateOrderDetail(mainOrder.OrderId, cartItem);
                            _unitOfWork.OrderDetail.Add(orderDetail);

                            // Update main order total amount
                            mainOrder.TotalAmount += (orderDetail.Price * (1 - (orderDetail.Sale ?? 0) / 100)) * orderDetail.Quantity;
                        }

                        // Save all changes
                        _unitOfWork.Save();
                    }
                    else
                    {
                        // Create the main order
                        var mainOrder = CreateOrder(userId, null, shippingAddress);
                        _unitOfWork.Order.Add(mainOrder);
                        _unitOfWork.Save();

                        // Create suborders for each vendor group
                        foreach (var vendorGroup in vendorGroups)
                        {
                            var subOrder = CreateOrder(userId, mainOrder.OrderId, shippingAddress);
                            subOrder.VendorId = vendorGroup.Key;
                            _unitOfWork.Order.Add(subOrder);
                            _unitOfWork.Save();

                            foreach (var cartItem in vendorGroup)
                            {
                                var orderDetail = CreateOrderDetail(subOrder.OrderId, cartItem);
                                _unitOfWork.OrderDetail.Add(orderDetail);

                                // Update suborder total amount
                                subOrder.TotalAmount += (orderDetail.Price * (1 - (orderDetail.Sale ?? 0) / 100)) * orderDetail.Quantity;
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


        private Order CreateOrder(string userId, Guid? parentOrderId, string shippingAddress)
        {
            return new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.PENDING,
                ParentOrderId = parentOrderId,
                TotalAmount = 0, // Will be updated later
                ShippingAddress = shippingAddress
            };
        }

        private OrderDetail CreateOrderDetail(Guid orderId, ShoppingCart cartItem)
        {
            return new OrderDetail
            {
                OrderId = orderId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price = (decimal)(cartItem.VariantOptionID.HasValue ? cartItem.ProductVariantOption!.Price : cartItem.Product.Price)!,
                Sale = cartItem.VariantOptionID.HasValue ? cartItem.ProductVariantOption!.Sale : cartItem.Product.Sale!,
                OptionName = cartItem.ProductVariantOption?.Value ?? null,
                OptionId = cartItem.VariantOptionID.HasValue ? cartItem.VariantOptionID : null

            };
        }
    }
}