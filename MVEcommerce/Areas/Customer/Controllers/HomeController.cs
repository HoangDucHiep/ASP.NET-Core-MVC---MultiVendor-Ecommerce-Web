using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.CategoryProduct;
using System.Diagnostics;
using MVEcommerce.Models.ViewModels.ProductDetailViewModel;
namespace MVEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [Route("category/{slug}")]
        public IActionResult ProductsByCategory(string slug)
        {
            
            var category = _unitOfWork.Category.GetBySlug(slug);
            if (category == null)
            {
                return NotFound(); 
            }
            
            var products = _unitOfWork.Product.GetAll(
                p => p.CategoryId== category.CategoryId,
                includeProperties: "Category,ProductImages"
			);
            
            var categoryProduct = new CategoryProduct
            {
                Products = products
            };

            return View(categoryProduct);
        }

		public IActionResult ProductDetail(string slug)
		{
           var product = _unitOfWork.Product.GetProductBySlug(slug); 

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
					ProductImages = product.ProductImages?.ToList()
				};

             
            return View(viewModel);
		}

		public IActionResult Index()
        {
            return View();
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
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages,ProductVariants").ToList();
            return Json(new { data = objProductList });
        }
        #endregion  
    }
}
