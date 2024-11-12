using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVEcommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public IActionResult Index()
        {
            return View();
        }
    }
}
