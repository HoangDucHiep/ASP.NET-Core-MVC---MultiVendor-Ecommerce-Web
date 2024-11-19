using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.Account;
using MVEcommerce.Models.ViewModels.Admin;
using MVEcommerce.Utility;
using System.Security.Claims;

namespace MVEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRole.ADMIN)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VendorSite()
        {
            var vendors = _unitOfWork.Vendor.GetAll().ToList();
            return View(vendors);
        }

        public IActionResult ProductSite()
        {
            var products = _unitOfWork.Product.GetAll(p=>p.Status != ProductStatus.DELETED, includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions");

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

            AdminProductVM vm = new AdminProductVM
            {
                Products = products,
                Options = products.SelectMany(p => p.ProductVariants!).SelectMany(pv => pv.ProductVariantOptions!)
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVendorStatus(int id, string status)
        {
            var vendor = _unitOfWork.Vendor.Get(c => c.VendorId == id);
            if (vendor == null)
            {
                return NotFound();
            }

            if (status == VendorStatus.ACTIVE)
            {
                vendor.Status = VendorStatus.ACTIVE;
                UpdateProductsAndOptionsStatus(vendor, ProductStatus.ACTIVE);
            }
            else if (status == VendorStatus.REJECTED)
            {
                vendor.Status = VendorStatus.REJECTED;

                // DELETE VENDOR
                _unitOfWork.Vendor.Remove(vendor);

                // Update user role
                var user = await _userManager.FindByIdAsync(vendor.UserId);
                if (user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, ApplicationRole.VENDOR);
                }
            }
            else if (status == VendorStatus.INACTIVE)
            {
                vendor.Status = VendorStatus.INACTIVE;
                UpdateProductsAndOptionsStatus(vendor, ProductStatus.INACTIVE);
            }

            _unitOfWork.Save();

            return RedirectToAction(nameof(VendorSite));
        }

        private void UpdateProductsAndOptionsStatus(Vendor vendor, string status)
        {
            var products = _unitOfWork.Product.GetAll(p => p.VendorId == vendor.VendorId).ToList();

            foreach (var product in products)
            {
                if (status == ProductStatus.ACTIVE && product.Stock <= 0)
                {
                    continue;
                }

                product.Status = status;

                if (product.HasVariant)
                {
                    var variants = _unitOfWork.ProductVariant.GetAll(v => v.ProductId == product.ProductId).ToList();
                    foreach (var variant in variants)
                    {
                        var options = _unitOfWork.ProductVariantOption.GetAll(o => o.VariantId == variant.VariantId).ToList();
                        foreach (var option in options)
                        {
                            if (status == ProductStatus.ACTIVE && option.Stock <= 0)
                            {
                                continue;
                            }

                            option.Status = status;
                            _unitOfWork.ProductVariantOption.Update(option);
                        }
                    }
                }

                _unitOfWork.Product.Update(product);
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProductStatus(int id, string status)
        {
            var product = _unitOfWork.Product.Get(c => c.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            if (status == ProductStatus.ACTIVE)
            {
                product.Status = ProductStatus.ACTIVE;
                UpdateOptionsStatus(product, ProductStatus.ACTIVE);
            }
            else if (status == ProductStatus.INACTIVE)
            {
                product.Status = ProductStatus.INACTIVE;
                UpdateOptionsStatus(product, ProductStatus.INACTIVE);
            }
            else if (status == ProductStatus.PENDING)
            {
                product.Status = ProductStatus.PENDING;
                UpdateOptionsStatus(product, ProductStatus.PENDING);
            }
            else if (status == ProductStatus.LOCKED)
            {
                product.Status = ProductStatus.LOCKED;
                UpdateOptionsStatus(product, ProductStatus.LOCKED);
            }

            _unitOfWork.Save();

            return RedirectToAction(nameof(ProductSite));
        }


        private void UpdateOptionsStatus(Product product, string status)
        {
            if (product.HasVariant)
            {
                var variants = _unitOfWork.ProductVariant.GetAll(v => v.ProductId == product.ProductId).ToList();
                foreach (var variant in variants)
                {
                    var options = _unitOfWork.ProductVariantOption.GetAll(o => o.VariantId == variant.VariantId).ToList();
                    foreach (var option in options)
                    {
                        option.Status = status;
                        _unitOfWork.ProductVariantOption.Update(option);
                    }
                }
            }
        }

        #region API

        [HttpGet]
        public IActionResult GetFilteredVendor(string status)
        {
            var vendors = _unitOfWork.Vendor.GetAll().ToList();

            if (status != "all")
            {
                vendors = vendors.Where(v => v.Status == status).ToList();
            }

            return PartialView("_AdminVendorAjaxPartial", vendors);
        }

        [HttpGet]
        public IActionResult GetFilteredProducts(string status)
        {
            var products = _unitOfWork.Product.GetAll(p=>p.Status != ProductStatus.DELETED, includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions");
            
            products = status?.ToLower() switch
            {
                ProductStatus.ACTIVE => products.Where(p => p.Status == ProductStatus.ACTIVE),
                ProductStatus.INACTIVE => products.Where(p => p.Status == ProductStatus.INACTIVE),
                ProductStatus.PENDING => products.Where(p => p.Status == ProductStatus.PENDING),
                ProductStatus.LOCKED => products.Where(p => p.Status == ProductStatus.LOCKED),
                _ => products
            };

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

            AdminProductVM vm = new AdminProductVM
            {
                Products = products,
                Options = products.SelectMany(p => p.ProductVariants!).SelectMany(pv => pv.ProductVariantOptions!)
            };
        
            return PartialView("_AdminProduct", vm);
        }
            
        #endregion
    }
}