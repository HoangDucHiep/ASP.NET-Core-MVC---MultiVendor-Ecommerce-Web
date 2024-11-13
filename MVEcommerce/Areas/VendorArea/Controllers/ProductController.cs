using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models.ViewModels.Account;

namespace MVEcommerce.Areas.VendorArea.Controllers
{
    [Area("VendorArea")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {      
            VendorProductIndexVM vm = new()
            {
                Products = _unitOfWork.Product.GetAll(null, includeProperties: "Category,ProductImages")
            };

            return View(vm);
        }
    }
}