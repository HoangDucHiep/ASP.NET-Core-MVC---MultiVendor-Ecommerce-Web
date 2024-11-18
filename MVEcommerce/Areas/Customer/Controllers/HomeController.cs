﻿using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.CategoryProduct;
using System.Diagnostics;
using MVEcommerce.Models.ViewModels.ProductDetailViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using MVEcommerce.Models.ViewModels.Account;
using MVEcommerce.Models.ViewModels.AddToCart;
using System.Security.Claims;
using MVEcommerce.Utility;

namespace MVEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
		[HttpGet]
		public IActionResult Search(string query)
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return View();
			}
			var products = _unitOfWork.Product.GetAll(
				p => p.Name.Contains(query) || p.Description.Contains(query),
				includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions");

            foreach (var product in products)
            {
                if (product.HasVariant)
                {
                    var variantOptions = product.ProductVariants!.SelectMany(v => v.ProductVariantOptions!);
                    var lowestPriceOption = variantOptions.OrderBy(vo => vo.Price).FirstOrDefault();
                    var totalStock = variantOptions.Sum(vo => vo.Stock);
                    product.Price = lowestPriceOption?.Price ?? product.Price;
                    product.Stock = totalStock;
                    product.Sale = lowestPriceOption?.Sale ?? 0;
                }
            }


            var categoryProduct = new CategoryProduct
			{
				Products = products
			};
			
			return PartialView("Search", categoryProduct);
		}

        [HttpGet]
        public IActionResult SearchProduct(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return View();
            }
            var products = _unitOfWork.Product.GetAll(
                p => p.Name.Contains(s) || p.Description.Contains(s),
                includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions");
            var categoryProduct = new CategoryProduct
            {
                Products = products
            };
            
            return View(categoryProduct);
        }

        [Route("category/{slug}")]
        public IActionResult ProductsByCategory(string slug)
        {
            var category = _unitOfWork.Category.Get(c=>c.Slug == slug);
            if (category == null)
            {
                return NotFound();
            }

            var products = _unitOfWork.Product.GetAll(
                p => p.CategoryId == category.CategoryId,
                includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions,Vendor"
			);

            foreach (var product in products)
            {
                if (product.HasVariant)
                {
                    var variantOptions = product.ProductVariants!.SelectMany(v => v.ProductVariantOptions!);
                    var lowestPriceOption = variantOptions.OrderBy(vo => vo.Price).FirstOrDefault();
                    var totalStock = variantOptions.Sum(vo => vo.Stock);
                    product.Price = lowestPriceOption?.Price ?? product.Price;
                    product.Stock = totalStock;
                    product.Sale = lowestPriceOption?.Sale ?? 0;
                }
            }

            var categoryProduct = new CategoryProduct
            {
                Products = products,
                CategoryName = category.Name
            };

            return View(categoryProduct);
        }

		[HttpGet]
		public IActionResult ShowProductModal(int productId, int? optionId)
		{
			var product = _unitOfWork.Product.GetAll(
				p => p.ProductId == productId,
				includeProperties: "Category,ProductImages,ProductVariants").FirstOrDefault();


			if (product == null)
			{

				return NotFound("Product not found.");
			}

            if (optionId != null)
            {
                var option = _unitOfWork.ProductVariantOption.Get(o => o.OptionId == optionId);
                product.Price = option.Price;
                product.Sale = option.Sale;
                product.Stock = 0; // dummy
            }

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

            var existingCartItem = _unitOfWork.ShoppingCart.GetAll(c => c.UserId == userId, "Product,ProductVariantOption");

            decimal totalAmount = 0;

            foreach (var cartItem in existingCartItem)
            {
                decimal Price = (decimal)(cartItem.VariantOptionID.HasValue ? cartItem.ProductVariantOption!.Price : cartItem.Product.Price)!;
                decimal Sale = (decimal)(cartItem.VariantOptionID.HasValue ? cartItem.ProductVariantOption!.Sale : cartItem.Product.Sale!)!;
                totalAmount += (Price * (1 - Sale / 100)) * cartItem.Quantity;
            }

            ViewBag.TotalAmount = totalAmount;
            ViewBag.Quantity = existingCartItem.Where(c=>c.ProductId==productId && c.VariantOptionID==optionId).Sum(c => c.Quantity);
            ViewBag.TotalItem = existingCartItem.Sum(c => c.Quantity);

            return PartialView("ShowProductModal", product);
		}

		[HttpPost]
        [Route("api/addToCart")]
        public IActionResult AddToCart([FromBody] AddToCart cartItem)
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

            var existingCartItem = _unitOfWork.ShoppingCart
                .GetAll(c => c.ProductId == cartItem.ProductId && c.UserId == userId && c.VariantOptionID == cartItem.VariantOptionID)
                .FirstOrDefault();

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
                _unitOfWork.ShoppingCart.Update(existingCartItem);
            }
            else
            {
                var newCartItem = new ShoppingCart
                {
                    ProductId = cartItem.ProductId,
                    UserId = userId,
                    Quantity = cartItem.Quantity > 0 ? cartItem.Quantity : 1,
                    VariantOptionID = cartItem.VariantOptionID
                };

                _unitOfWork.ShoppingCart.Add(newCartItem);
            }

            _unitOfWork.Save();

            return Json(new { success = true });
        }

	[Route("ProductDetail/{slug}")]
		public IActionResult ProductDetail(string slug)
		{
			var product = _unitOfWork.Product.Get(p=>p.Slug == slug, includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions,ProductImages,Vendor");

			if (product == null)
			{
				return NotFound();
			}

            var viewModel = new ProductDetailViewModel
            {
                product = product,
                ProductImage = product.ProductImages?.FirstOrDefault(),
                productVariant = product.ProductVariants?.FirstOrDefault(),
                productVariantOption = product.ProductVariants?.FirstOrDefault()?.ProductVariantOptions?.FirstOrDefault(),
                category = product.Category,
                ProductImages = product.ProductImages?
                    .GroupBy(x => x.VariantOptionID)
                    .Select(group => group.FirstOrDefault())
                    .ToList(),
                AllProductImages = product.ProductImages?.ToList(),
                Vendor = product.Vendor

            };
			return View(viewModel);
		}



		public IActionResult Index()
        {

            var products = _unitOfWork.Product.GetAll(p => p.Status == ProductStatus.ACTIVE, includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions,ProductImages,Vendor").OrderBy(p => p.Sale).Reverse().ToList(); ;

            foreach (var product in products)
            {
                if (product.HasVariant)
                {
                    var variantOptions = product.ProductVariants!.SelectMany(v => v.ProductVariantOptions!);
                    var lowestPriceOption = variantOptions.OrderBy(vo => vo.Price).FirstOrDefault();
                    var totalStock = variantOptions.Sum(vo => vo.Stock);

                    product.Price = lowestPriceOption?.Price ?? product.Price;
                    product.Stock = totalStock;
                    product.Sale = lowestPriceOption?.Sale ?? 0;
                }
            }

            products = products.Where(p=>p.Sale > 0).Take(20).ToList();

            return View(products);
        }

        public IActionResult VendorPage(int vendorId)
        {
            ViewBag.Vendor = _unitOfWork.Vendor.Get(p => p.VendorId == vendorId, "Addresses");
            var lstProduct= _unitOfWork.Product.GetAll(p=>p.Vendor.VendorId==vendorId,includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions,Vendor,ProductImages").ToList();
			foreach (var product in lstProduct)
			{
				if (product.HasVariant)
				{
					var variantOptions = product.ProductVariants!.SelectMany(v => v.ProductVariantOptions!);
					var lowestPriceOption = variantOptions.OrderBy(vo => vo.Price).FirstOrDefault();
					var totalStock = variantOptions.Sum(vo => vo.Stock);

					product.Price = lowestPriceOption?.Price ?? product.Price;
					product.Stock = totalStock;
					product.Sale = lowestPriceOption?.Sale ?? 0;
				}
			}

			
			return View(lstProduct);
        }


		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
            return View();
        }

        #region API CALLS
        //call url: /customer/home/getall
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> objProductList = _unitOfWork.Category.GetAll(includeProperties: "Products").ToList();
            return Json(new { data = objProductList });
        }

        //call url: /customer/home/GetAllProduct
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "ProductImages,ProductVariants.ProductVariantOptions.ProductImages").ToList();
            return Json(new { data = objProductList });
        }

        // API để upload file
        [HttpPost]
        [Route("api/upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{file.FileName}";
            return Ok(new { url = fileUrl });
        }

        // API để xóa file
        [HttpDelete]
        [Route("api/delete")]
        public IActionResult Delete([FromBody] FileDeleteRequest request)
        {
            if (string.IsNullOrEmpty(request.Url))
            {
                return BadRequest("No file URL provided.");
            }

            var fileName = Path.GetFileName(request.Url);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Ok(new { message = "File deleted successfully." });
            }

            return NotFound("File not found.");
        }
        #endregion  
    }

    public class FileDeleteRequest
    {
        public string Url { get; set; }
    }
}