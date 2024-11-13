using Microsoft.AspNetCore.Mvc;

namespace MVEcommerce.Areas.VendorArea.Controllers
{
    [Area("VendorArea")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
