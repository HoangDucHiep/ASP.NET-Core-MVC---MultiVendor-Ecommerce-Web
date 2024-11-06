using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;

namespace MVEcommerce.Areas.Customer.Controllers
{
    public class DatController : Controller
    {

        //private readonly IUnitOfWork _unitOfWork;

        //public DatController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public IActionResult Index()
        {

            //var categories = _unitOfWork.Category.GetAll();

            return View();
        }
    }
}
