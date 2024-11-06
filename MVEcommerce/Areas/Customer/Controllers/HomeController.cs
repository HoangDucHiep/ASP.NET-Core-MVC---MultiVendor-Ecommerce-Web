using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using System.Diagnostics;

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

        public IActionResult DatDongQuang()
        {
            var categories = _unitOfWork.Category.GetAll();
            return View(categories);
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
            List<Product> objProductList = _unitOfWork.Product.GetAll("Category,Vendor").ToList();
            return Json(new { data = objProductList });
        }
        #endregion  
    }
}
