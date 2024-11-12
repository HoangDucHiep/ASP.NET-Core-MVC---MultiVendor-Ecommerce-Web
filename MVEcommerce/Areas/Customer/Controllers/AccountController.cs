using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using System.Security.Claims;

namespace MVEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public IActionResult BecomeVendor()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Vendor vendor = new Vendor()
            { UserId = userId };

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
