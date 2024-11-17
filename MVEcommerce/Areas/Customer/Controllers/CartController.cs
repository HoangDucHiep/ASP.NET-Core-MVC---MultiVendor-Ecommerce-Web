using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.ShoopingCart;
using System.Security.Claims;

namespace MVEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<HomeController> _logger;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public CartController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_hostingEnvironment = hostingEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ShowCart()
		{
			if (User.Identity is not ClaimsIdentity claimsIdentity)
			{
				return Unauthorized(new { success = false, message = "Bạn chưa đăng nhập!" });
			}

			var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			if (userIdClaim == null)
			{
				return Unauthorized(new { success = false, message = "Không tìm thấy thông tin người dùng!" });
			}

			var userId = userIdClaim.Value;

			var cartItems = _unitOfWork.ShoppingCart
				.GetAll(c => c.UserId == userId, includeProperties: "Product,ProductVariantOption,Product.ProductImages,ProductVariantOption.ProductImages,Product.Vendor")
				.Select(cartItem =>
				{
					var product = cartItem.Product;
					var variantOption = cartItem.ProductVariantOption;

					var price = variantOption?.Price ?? product.Price ?? 0;
					var sale = variantOption?.Sale ?? product.Sale ?? 0;
					var discountedPrice = price - (price * sale / 100);

					var imageUrl = variantOption?.ProductImages?.FirstOrDefault()?.ImageUrl
								   ?? product.ProductImages?.FirstOrDefault()?.ImageUrl;


					return new CartItemViewModel
					{
						ProductId = product.ProductId,
						ProductName = product.Name,
						VariantName = variantOption?.Value,
						Price = discountedPrice,
						Quantity = cartItem.Quantity,
						TotalPrice = discountedPrice * cartItem.Quantity,
						ImageUrl = imageUrl,
						Stock = variantOption?.Stock ?? product.Stock ?? 0,
						VendorName = product.Vendor?.Name ?? "Unknown Vendor",
						CartId = cartItem.CartId,
						VariantOptionID = variantOption?.VariantId ?? null

					};
				})
				.ToList();

			var showProductCart = new ShowProductCart
			{
				ListCart = cartItems,
				TotalPrice=cartItems.Sum(x => x.TotalPrice)
			};

			return View(showProductCart);
		}

		[HttpPost]
		[Route("api/UpdateQuantity")]
        public IActionResult UpdateQuantity(int cartId, int quantity, int? optionId = null)
        {
            var cartItem = _unitOfWork.ShoppingCart.GetAll(c => c.CartId == cartId && c.VariantOptionID == optionId).FirstOrDefault();

            if (cartItem == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });
            }

            cartItem.Quantity = quantity;

            if (quantity == 0)
            {      
                _unitOfWork.ShoppingCart.Remove(cartItem);
            }

			cartItem.VariantOptionID = optionId;

            _unitOfWork.Save();

            return Json(new {  success = true });
        }


        [HttpPost]
		[Route("api/RemoveFromCart")]
        public IActionResult RemoveFromCart(int cartId, int? optionId = null)
        {
            var cartItem = _unitOfWork.ShoppingCart.GetAll(c => c.CartId == cartId&& c.VariantOptionID ==optionId ).FirstOrDefault();

            if (cartItem == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });
            }

            _unitOfWork.ShoppingCart.Remove(cartItem);
            _unitOfWork.Save();

           

            return Json(new
            {
                success = true
               
            });
        }



    }
}
