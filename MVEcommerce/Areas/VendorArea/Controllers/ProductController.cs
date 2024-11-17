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
using MVEcommerce.Utility;
using Newtonsoft.Json;

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
            var products = _unitOfWork.Product.GetAll(p => p.Status == ProductStatus.ACTIVE, includeProperties: "Category,ProductImages,ProductVariants.ProductVariantOptions");

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

        public IActionResult AddProduct(VendorAddProductVM? vm)
        {
            if (vm == null)
            {
                vm = new VendorAddProductVM()
                {
                    Product = new Product(),
                    Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.CategoryId.ToString()
                    })
                };
            }
            else
            {
                vm.Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                });
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(VendorAddProductVM vm, IFormFile? mainImage,
            List<IFormFile>? additionalImages)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        additionalImages ??= new List<IFormFile>();

                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var vendor = _unitOfWork.Vendor.Get(v => v.UserId == userId);

                        if (vendor != null)
                        {
                            vm.Product.VendorId = vendor.VendorId;
                            if (string.IsNullOrEmpty(vm.ProductVariant.Name))
                            {
                                vm.Product.HasVariant = false;
                            }
                            else
                            {   
                                vm.Product.HasVariant = true;
                                vm.Product.Price = null;
                                vm.Product.Sale = null;
                                vm.Product.Stock = null;
                            }
                            
                            vm.Product.Slug = _slugHelper.GenerateSlug(vm.Product.Name ?? "");
                            //@TODO: fix later
                            vm.Product.Status = ProductStatus.ACTIVE;

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
                                    if (option.OptionImages != null && option.OptionImages.Count > 0)
                                    {
                                        foreach (var image in option.OptionImages)
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

        public IActionResult EditProduct(int id)
        {
            var product = _unitOfWork.Product.Get(p => p.ProductId == id, includeProperties: "ProductImages,ProductVariants.ProductVariantOptions");
            if (product == null)
            {
                return NotFound();
            }

            var vm = new VendorEditProductVM
            {
                Product = product,
                Categories = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                }),
                ProductVariant = product.ProductVariants!.FirstOrDefault() ?? new ProductVariant(),
                ProductVariantOptions = product?.ProductVariants?
                    .SelectMany(v => v.ProductVariantOptions ?? new List<ProductVariantOption>())
                    .Select(o => new ProductVariantOptionVM
                    {
                        OptionId = o.OptionId,
                        VariantId = o.VariantId,
                        Value = o.Value,
                        Price = o.Price,
                        Sale = o.Sale,
                        Stock = o.Stock,
                        SKU = o.SKU,
                        Status = o.Status,
                        CreatedAt = o.CreatedAt,
                        UpdatedAt = o.UpdatedAt,
                        ProductImages = o.ProductImages?.ToList() ?? new List<ProductImage>()
                    }).ToList() ?? new List<ProductVariantOptionVM>()

            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(VendorEditProductVM vm, IFormFile? mainImage, List<IFormFile>? additionalImages, string? deleteImages, string? deletedOptions)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var product = _unitOfWork.Product.Get(p => p.ProductId == vm.Product.ProductId, includeProperties: "ProductImages,ProductVariants.ProductVariantOptions");

                        if (product == null)
                        {
                            return NotFound();
                        }

                        // Update product details
                        product.Name = vm.Product.Name;
                        product.SKU = vm.Product.SKU;
                        product.Price = vm.Product.Price;
                        product.Sale = vm.Product.Sale;
                        product.Stock = vm.Product.Stock;
                        product.Status = vm.Product.Status;
                        product.Description = vm.Product.Description;
                        product.CategoryId = vm.Product.CategoryId;
                        product.HasVariant = vm.ProductVariantOptions.Count > 0 ? true : false;

                        if (product.HasVariant)
                        {
                            product.Price = null;
                            product.Sale = null;
                            product.Stock = null;
                        }

                        // Update main image
                        if (mainImage != null)
                        {
                            var mainImageEntity = product.ProductImages.FirstOrDefault(i => i.IsMain);
                            if (mainImageEntity != null)
                            {
                                _unitOfWork.ProductImage.Remove(mainImageEntity);
                            }

                            var mainImageUrl = await SaveImage(mainImage);
                            product.ProductImages.Add(new ProductImage
                            {
                                ImageUrl = mainImageUrl,
                                IsMain = true,
                                ProductId = product.ProductId
                            });
                        }

                        // Add additional images
                        if (additionalImages != null)
                        {
                            foreach (var image in additionalImages)
                            {
                                var imageUrl = await SaveImage(image);
                                product.ProductImages.Add(new ProductImage
                                {
                                    ImageUrl = imageUrl,
                                    IsMain = false,
                                    ProductId = product.ProductId
                                });
                            }
                        }

                        // Remove images
                        if (!string.IsNullOrEmpty(deleteImages))
                        {
                            var imageIds = JsonConvert.DeserializeObject<List<int>>(deleteImages);
                            foreach (var imageId in imageIds)
                            {
                                var image = product.ProductImages.FirstOrDefault(i => i.ImageId == imageId);
                                if (image != null)
                                {
                                    _unitOfWork.ProductImage.Remove(image);
                                    DeleteImage(image.ImageUrl);
                                }
                            }
                        }

                        // Update or add variant
                        if (vm.ProductVariant != null && !string.IsNullOrEmpty(vm.ProductVariant.Name))
                        {
                            var existingVariant = product.ProductVariants.FirstOrDefault();
                            if (existingVariant != null)
                            {
                                existingVariant.Name = vm.ProductVariant.Name;
                                _unitOfWork.ProductVariant.Update(existingVariant);
                            }
                            else
                            {
                                vm.ProductVariant.ProductId = product.ProductId;
                                var entry = _dbContext.ProductVariants.Add(vm.ProductVariant);
                                _unitOfWork.Save();

                                // The ID will be populated in the entity
                                vm.ProductVariant.VariantId = entry.Entity.VariantId;
                            }

                            // Update or add options
                            for (int i = 0; i < vm.ProductVariantOptions.Count; i++)
                            {
                                var option = vm.ProductVariantOptions[i];
                                if (option.OptionId > 0)
                                {
                                    var existingOption = _unitOfWork.ProductVariantOption.Get(o => o.OptionId == option.OptionId);
                                    if (existingOption != null)
                                    {
                                        existingOption.Value = option.Value;
                                        existingOption.Price = option.Price;
                                        existingOption.Stock = option.Stock;
                                        existingOption.SKU = option.SKU;
                                        existingOption.Sale = option.Sale;
                                        _unitOfWork.ProductVariantOption.Update(existingOption);
                                    }
                                }
                                else
                                {
                                    option.VariantId = vm.ProductVariant.VariantId;
                                    var entry = _dbContext.ProductVariantsOption.Add(option);
                                    _dbContext.SaveChanges();

                                    // The ID will be populated in the entry's entity
                                    option.OptionId = entry.Entity.OptionId; // This will have the correct ID
                                }

                                // Handle option images
                                if (option.OptionImages != null && option.OptionImages.Count > 0)
                                {
                                    foreach (var image in option.OptionImages)
                                    {
                                        string fileName = await SaveImage(image);
                                        _unitOfWork.ProductImage.Add(new ProductImage
                                        {
                                            ProductId = vm.Product.ProductId,
                                            VariantOptionID = option.OptionId, // Now using correct ID from tracked entity
                                            ImageUrl = fileName
                                        });
                                    }
                                    _unitOfWork.Save();
                                }
                            }
                        }
                        else
                        {
                            // Remove existing variant and its options
                            var existingVariant = _unitOfWork.ProductVariant.Get(u=>u.ProductId == product.ProductId, "ProductVariantOptions.ProductImages");
                            if (existingVariant != null)
                            {
                                // Remove options and their images
                                foreach (var option in existingVariant.ProductVariantOptions)
                                {
                                    foreach (var image in option.ProductImages)
                                    {
                                        DeleteImage(image.ImageUrl);
                                        _unitOfWork.ProductImage.Remove(image);
                                    }
                                    _unitOfWork.ProductVariantOption.Remove(option);
                                }
                                _unitOfWork.ProductVariant.Remove(existingVariant);
                            }
                        }

                        // Remove deleted options and their images
                        if (!string.IsNullOrEmpty(deletedOptions))
                        {
                            var optionIds = JsonConvert.DeserializeObject<List<int>>(deletedOptions);
                            foreach (var optionId in optionIds)
                            {
                                var deletedOption = _unitOfWork.ProductVariantOption.Get(
                                    o => o.OptionId == optionId,
                                    includeProperties: "ProductImages"); // Include related images

                                if (deletedOption != null)
                                {
                                    // Remove physical image files first
                                    if (deletedOption.ProductImages != null)
                                    {
                                        foreach (var image in deletedOption.ProductImages)
                                        {
                                            // Delete physical file
                                            DeleteImage(image.ImageUrl);
                                            // Remove from database
                                            _unitOfWork.ProductImage.Remove(image);
                                        }
                                    }

                                    // Remove the option itself
                                    _unitOfWork.ProductVariantOption.Remove(deletedOption);
                                }
                            }
                        }


                        // Save changes
                        _unitOfWork.Save();
                        transaction.Commit();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
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

        [HttpPost]
        public IActionResult DeleteVariant(int variantId)
        {
            var variant = _unitOfWork.ProductVariant.Get(v => v.VariantId == variantId, includeProperties: "ProductVariantOptions.ProductImages");
            if (variant != null)
            {
                // Xóa các ProductImages liên quan đến các ProductVariantOption
                foreach (var option in variant.ProductVariantOptions)
                {
                    foreach (var image in option.ProductImages)
                    {
                        DeleteImage(image.ImageUrl);
                        _unitOfWork.ProductImage.Remove(image);
                    }
                }
                // Cập nhật trạng thái của variant thành "Deleted"
                variant.Status = "Deleted";
                _unitOfWork.ProductVariant.Update(variant);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteOption(int optionId)
        {
            var option = _unitOfWork.ProductVariantOption.Get(o => o.OptionId == optionId, includeProperties: "ProductImages");
            if (option != null)
            {
                // Xóa các ProductImages liên quan đến option
                foreach (var image in option.ProductImages)
                {
                    DeleteImage(image.ImageUrl);
                    _unitOfWork.ProductImage.Remove(image);
                }
                // Cập nhật trạng thái của option thành "Deleted"
                option.Status = "Deleted";
                _unitOfWork.ProductVariantOption.Update(option);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteImage(int imageId)
        {
            var image = _unitOfWork.ProductImage.Get(i => i.ImageId == imageId);
            if (image != null)
            {
                DeleteImage(image.ImageUrl);
                _unitOfWork.ProductImage.Remove(image);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }



        private void DeleteImage(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));
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