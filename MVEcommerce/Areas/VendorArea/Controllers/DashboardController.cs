using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models.ViewModels.VendorDetails;
using MVEcommerce.Utility;
using MVEcommerce.Models;

namespace MVEcommerce.Areas.VendorArea.Controllers
{
    [Area("VendorArea")]
    [Authorize(Roles = ApplicationRole.VENDOR)]
    public class DashboardController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditShop()
        {
            // get current user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vendor = _unitOfWork.Vendor.Get(v => v.UserId == userId, "User") ?? new Vendor();
            if (vendor == null)
            {
                return NotFound();
            }
            var Address = _unitOfWork.Address.Get(a=>a.VendorId == vendor.VendorId) ?? new Address();

            Address.Email = vendor?.User?.Email!;
            Address.PhoneNumber = vendor?.User?.PhoneNumber!;

            var vendorDetailVM = new VendorDetailVM
            {
                Vendor = vendor,
                Address = Address
            };

            return View(vendorDetailVM);
        }
    }
}
