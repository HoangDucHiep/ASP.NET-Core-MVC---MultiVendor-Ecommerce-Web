using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.VendorProduct;
using Slugify;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MVEcommerce.DataAccess.Data;
using MVEcommerce.Models.ViewModels.Account;

namespace MVEcommerce.Areas.VendorArea.Controllers
{
    [Area("VendorArea")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SlugHelper _slugHelper;
        private readonly ApplicationDbContext _dbContext;

        public ProductController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _slugHelper = new SlugHelper();
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions");
        
            foreach (var product in products)
            {
                if (product.HasVariant)
                {
                    var variantOptions = product.ProductVariants!.SelectMany(v => v.ProductVariantOptions!);
                    var lowestPriceOption = variantOptions.OrderBy(vo => vo.Price).FirstOrDefault();
                    var totalStock = variantOptions.Sum(vo => vo.Stock);
        
                    product.Price = lowestPriceOption?.Price ?? product.Price;
                    product.Stock = totalStock;
                    product.Sale = lowestPriceOption.Sale;
                }
            }
        
            VendorProductIndexVM vm = new VendorProductIndexVM
            {
                Products = products,
                Options = products.SelectMany(p => p.ProductVariants!).SelectMany(pv => pv.ProductVariantOptions!)
            };
        
            return View(vm);
        }

        public IActionResult AddProduct()
        {
            VendorAddProductVM vm = new VendorAddProductVM
            {
                Product = new Product(),
                Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                })
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(VendorAddProductVM vm)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var vendor = _unitOfWork.Vendor.Get(v => v.UserId == userId);

                        if (vendor != null)
                        {
                            // Kiểm tra nếu người dùng thêm ProductVariant thì cập nhật HasVariant thành true
                            if (vm.ProductVariant != null)
                            {
                                vm.Product.HasVariant = true;
                            }

                            // Thêm Product và lưu để lấy ProductId
                            vm.Product.VendorId = vendor.VendorId;
                            _unitOfWork.Product.Add(vm.Product);
                            _unitOfWork.Save(); // Tại đây, ProductId sẽ được tự động tăng và gán giá trị mới

                            // Tạo slug sau khi ProductId đã được gán giá trị mới
                            vm.Product.Slug = _slugHelper.GenerateSlug($"{vm.Product.Name}-{vm.Product.ProductId}");
                            _unitOfWork.Product.Update(vm.Product);
                            _unitOfWork.Save();

                            // Thêm ProductVariant và lưu để lấy VariantId
                            vm.ProductVariant.ProductId = vm.Product.ProductId;
                            _unitOfWork.ProductVariant.Add(vm.ProductVariant);
                            _unitOfWork.Save(); // Tại đây, VariantId sẽ được tự động tăng và gán giá trị mới

                            // Thêm ProductVariantOption với VariantId đã được cập nhật
                            foreach (var option in vm.ProductVariantOptions)
                            {
                                option.VariantId = vm.ProductVariant.VariantId;
                                _unitOfWork.ProductVariantOption.Add(option);
                            }
                            _unitOfWork.Save(); // Lưu tất cả các thay đổi

                            transaction.Commit(); // Commit transaction nếu tất cả các thao tác thành công

                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError("", "Vendor not found for the current user.");
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Rollback transaction nếu có lỗi xảy ra
                        ModelState.AddModelError("", "An error occurred while saving the product. Please try again.");
                    }
                }
            }

            vm.Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });

            return View(vm);
        }
    }
}