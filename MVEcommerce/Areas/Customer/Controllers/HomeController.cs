using Microsoft.AspNetCore.Mvc;
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
                includeProperties: "Category,ProductImages"
            );

            var categoryProduct = new CategoryProduct
            {
                Products = products
            };

            return View(categoryProduct);
        }

		[HttpGet]
		public IActionResult ShowProductModal(int productId)
		{
			var product = _unitOfWork.Product.GetAll(
				p => p.ProductId == productId,
				includeProperties: "Category,ProductImages,ProductVariants").FirstOrDefault();

			if (product == null)
			{

				return NotFound("Product not found.");
			}


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

            return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng!" });
        }


		public IActionResult ProductDetail(string slug)
        {
               var product = _unitOfWork.Product.Get(c=>c.Slug == slug); 

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
                        ProductImages = product.ProductImages?.ToList(),
            };


            return View(viewModel);
        }

        public IActionResult Index()
        {

            var products = _unitOfWork.Product.GetAll(p => p.Status == "active", includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions,ProductImages,Vendor").OrderBy(p => p.Sale).Reverse().ToList(); ;

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

            var lstSaleProducts = new List<Product>();
            foreach (var product in products)
            {
                if (product.Sale > 0)
                {
                    lstSaleProducts.Add(product);

                }
            }



            return View(lstSaleProducts);



        }
       
        public IActionResult MyAccount()
        {   

            return View();
        }

        public IActionResult VendorPage(int vendorId)
        {
            ViewBag.VendorName = _unitOfWork.Vendor.Get(p => p.VendorId == vendorId).Name;
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