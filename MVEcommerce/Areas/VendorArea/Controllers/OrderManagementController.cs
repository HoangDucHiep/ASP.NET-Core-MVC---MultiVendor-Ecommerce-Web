using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.Account;
using MVEcommerce.Utility;
using Slugify;
using System.Security.Claims;

namespace MVEcommerce.Areas.VendorArea.Controllers
{
    [Area("VendorArea")]
    [Authorize]
    public class OrderManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SlugHelper _slugHelper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private Vendor currentVendor;

        public OrderManagementController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager,
            ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var userId = claim.Value;
                currentVendor = _unitOfWork.Vendor.Get(v => v.UserId == userId);
            }
        }

        public IActionResult Index()
        {
            if (currentVendor == null)
            {
                return Unauthorized();
            }

            var orders = _unitOfWork.Order.GetAll(filter: o => o.VendorId == currentVendor.VendorId, includeProperties: "User,OrderDetails,OrderDetails.Product.ProductImages");

            return View(orders);
        }


        public IActionResult OrderDetail(Guid id)
        {
            var order = _unitOfWork.Order.Get(filter: o => o.OrderId == id && o.VendorId == currentVendor.VendorId, includeProperties: "User,OrderDetails,OrderDetails.Product.ProductImages");
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(Order updateOrder)
        {
            var order = _unitOfWork.Order.Get(filter: o => o.OrderId == updateOrder.OrderId && o.VendorId == currentVendor.VendorId);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = updateOrder.Status;
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
