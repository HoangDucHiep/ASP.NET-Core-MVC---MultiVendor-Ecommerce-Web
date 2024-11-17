using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models.ViewModels.VendorDetails;
using MVEcommerce.Utility;
using MVEcommerce.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MVEcommerce.Areas.VendorArea.Controllers
{
    [Area("VendorArea")]
    [Authorize(Roles = ApplicationRole.VENDOR)]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DashboardController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
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
            var address = _unitOfWork.Address.Get(a => a.VendorId == vendor.VendorId) ?? new Address();

            address.Email = vendor?.User?.Email!;
            address.PhoneNumber = vendor?.User?.PhoneNumber!;

            var vendorDetailVM = new VendorDetailVM
            {
                Vendor = vendor,
                Address = address
            };

            return View(vendorDetailVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditShop(VendorDetailVM vendorDetailVM, IFormFile avatarFile, IFormFile bannerFile)
        {
            if (ModelState.IsValid)
            {
                var vendor = vendorDetailVM.Vendor;
                var address = vendorDetailVM.Address;

                if (avatarFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);
                    string uploads = Path.Combine(wwwRootPath, @"images\vendors");
                    string fullPath = Path.Combine(uploads, fileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        avatarFile.CopyTo(fileStream);
                    }

                    vendor.Avartar = @"\images\vendors\" + fileName;
                }

                if (bannerFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(bannerFile.FileName);
                    string uploads = Path.Combine(wwwRootPath, @"images\vendors");
                    string fullPath = Path.Combine(uploads, fileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        bannerFile.CopyTo(fileStream);
                    }

                    vendor.Banner = @"\images\vendors\" + fileName;
                }

                if (vendor.VendorId == 0)
                {
                    // get current user ID
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    vendor.UserId = userId;
                    _unitOfWork.Vendor.Add(vendor);
                }
                else
                {
                    _unitOfWork.Vendor.Update(vendor);
                }

                if (address.AddressId == 0)
                {
                    address.VendorId = vendor.VendorId;
                    _unitOfWork.Address.Add(address);
                }
                else
                {
                    _unitOfWork.Address.Update(address);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorDetailVM);
        }
    }
}