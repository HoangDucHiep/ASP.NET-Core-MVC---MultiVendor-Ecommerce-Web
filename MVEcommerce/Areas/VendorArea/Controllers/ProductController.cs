using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models.ViewModels.Account;
using System.Linq;

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
            var products = _unitOfWork.Product.GetAll(includeProperties: "ProductVariants.ProductVariantOptions");
        
            foreach (var product in products)
            {
                if (product.HasVariant)
                {
                    var variantOptions = product.ProductVariants!.SelectMany(v => v.ProductVariantOptions!);
                    var lowestPriceOption = variantOptions.OrderBy(vo => vo.Price).FirstOrDefault();
                    var totalStock = variantOptions.Sum(vo => vo.Stock);
        
                    product.Price = lowestPriceOption?.Price ?? product.Price;
                    product.Stock = totalStock;
                    product.SKU = lowestPriceOption?.SKU ?? product.SKU;
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
            return View();
        }
    }
}