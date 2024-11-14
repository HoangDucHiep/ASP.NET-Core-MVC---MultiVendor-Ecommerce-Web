using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.VendorProduct;
using Slugify;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, 
            ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _slugHelper = new SlugHelper();
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
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
                    product.Sale = lowestPriceOption?.Sale ?? 0;
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
        public async Task<IActionResult> AddProduct(VendorAddProductVM vm, IFormFile? mainImage, 
            List<IFormFile>? additionalImages, List<List<IFormFile>>? optionImages)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        additionalImages ??= new List<IFormFile>();
                        optionImages ??= new List<List<IFormFile>>();

                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var vendor = _unitOfWork.Vendor.Get(v => v.UserId == userId);

                        if (vendor != null)
                        {
                            vm.Product.VendorId = vendor.VendorId;
                            vm.Product.HasVariant = !string.IsNullOrEmpty(vm.ProductVariant.Name);
                            vm.Product.Slug = _slugHelper.GenerateSlug(vm.Product.Name ?? "");

                            // Save product first to get ID
                            _unitOfWork.Product.Add(vm.Product);
                            _unitOfWork.Save();

                            // Handle main image
                            if (mainImage != null)
                            {
                                string fileName = await SaveImage(mainImage);
                                _unitOfWork.ProductImage.Add(new ProductImage
                                {
                                    ProductId = vm.Product.ProductId,
                                    ImageUrl = fileName,
                                    IsMain = true
                                });
                                _unitOfWork.Save();
                            }

                            // Handle additional images
                            foreach (var image in additionalImages)
                            {
                                string fileName = await SaveImage(image);
                                _unitOfWork.ProductImage.Add(new ProductImage
                                {
                                    ProductId = vm.Product.ProductId,
                                    ImageUrl = fileName,
                                    IsMain = false
                                });
                            }
                            _unitOfWork.Save();

                            // Handle variant and options if exists
                            if (vm.Product.HasVariant)
                            {
                                vm.ProductVariant.ProductId = vm.Product.ProductId;
                                _unitOfWork.ProductVariant.Add(vm.ProductVariant);
                                _unitOfWork.Save();

                                // Handle options and their images
                                for (int i = 0; i < vm.ProductVariantOptions.Count; i++)
                                {
                                    var option = vm.ProductVariantOptions[i];
                                    option.VariantId = vm.ProductVariant.VariantId;
                                    _unitOfWork.ProductVariantOption.Add(option);
                                    _unitOfWork.Save();

                                    // Handle option images
                                    if (optionImages.Count > i)
                                    {
                                        foreach (var image in optionImages[i])
                                        {
                                            string fileName = await SaveImage(image);
                                            _unitOfWork.ProductImage.Add(new ProductImage
                                            {
                                                ProductId = vm.Product.ProductId,
                                                VariantOptionID = option.OptionId,
                                                ImageUrl = fileName
                                            });
                                        }
                                        _unitOfWork.Save();
                                    }
                                }
                            }

                            transaction.Commit();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", $"Error saving product: {ex.Message}");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            vm.Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            });
            return View(vm);
        }

        
        
        private void DeleteImage(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
                
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            
            return "/uploads/" + uniqueFileName;
        }
    }
}