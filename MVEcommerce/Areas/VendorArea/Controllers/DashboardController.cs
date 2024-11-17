using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models.ViewModels.VendorDetails;
using MVEcommerce.Utility;
using MVEcommerce.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage;

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
        public IActionResult EditShop(VendorDetailVM vendorDetailVM, IFormFile? avatarFile, IFormFile? bannerFile)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var vendor = _unitOfWork.Vendor.Get(v => v.VendorId == vendorDetailVM.Vendor.VendorId);
                        var address = _unitOfWork.Address.Get(a => a.AddressId == vendorDetailVM.Address.AddressId);

                        if (vendor == null || address == null)
                        {
                            return NotFound();
                        }

                        // Handle avatar file upload
                        if (avatarFile != null)
                        {
                            var avatarPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "avatars");
                            var avatarFileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);
                            var avatarFilePath = Path.Combine(avatarPath, avatarFileName);

                            // Delete old avatar if exists
                            if (!string.IsNullOrEmpty(vendor.Avartar))
                            {
                                var oldAvatarPath = Path.Combine(_hostEnvironment.WebRootPath, vendor.Avartar.TrimStart('/'));
                                if (System.IO.File.Exists(oldAvatarPath))
                                {
                                    System.IO.File.Delete(oldAvatarPath);
                                }
                            }

                            // Ensure the directory exists
                            if (!Directory.Exists(avatarPath))
                            {
                                Directory.CreateDirectory(avatarPath);
                            }

                            using (var fileStream = new FileStream(avatarFilePath, FileMode.Create))
                            {
                                avatarFile.CopyTo(fileStream);
                            }

                            vendor.Avartar = "/images/avatars/" + avatarFileName;
                        }

                        // Handle banner file upload
                        if (bannerFile != null)
                        {
                            var bannerPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "banners");
                            var bannerFileName = Guid.NewGuid().ToString() + Path.GetExtension(bannerFile.FileName);
                            var bannerFilePath = Path.Combine(bannerPath, bannerFileName);

                            // Ensure the directory exists
                            if (!Directory.Exists(bannerPath))
                            {
                                Directory.CreateDirectory(bannerPath);
                            }

                            // Delete old banner if exists
                            if (!string.IsNullOrEmpty(vendor.Banner))
                            {
                                var oldBannerPath = Path.Combine(_hostEnvironment.WebRootPath, vendor.Banner.TrimStart('/'));
                                if (System.IO.File.Exists(oldBannerPath))
                                {
                                    System.IO.File.Delete(oldBannerPath);
                                }
                            }

                            using (var fileStream = new FileStream(bannerFilePath, FileMode.Create))
                            {
                                bannerFile.CopyTo(fileStream);
                            }

                            vendor.Banner = "/images/banners/" + bannerFileName;
                        }

                        // Update vendor and address details
                        vendor.Name = vendorDetailVM.Vendor.Name;
                        vendor.UpdatedAt = DateTime.Now;

                        address.Country = vendorDetailVM.Address.Country;
                        address.City = vendorDetailVM.Address.City;
                        address.Street = vendorDetailVM.Address.Street;
                        address.Apartment = vendorDetailVM.Address.Apartment;
                        address.ZipCode = vendorDetailVM.Address.ZipCode;
                        address.Email = vendorDetailVM.Address.Email;
                        address.PhoneNumber = vendorDetailVM.Address.PhoneNumber;
                        address.VendorId = vendor.VendorId;

                        _unitOfWork.Vendor.Update(vendor);
                        _unitOfWork.Address.Update(address);
                        _unitOfWork.Save();

                        transaction.Commit();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return View(vendorDetailVM);
        }
    }
}