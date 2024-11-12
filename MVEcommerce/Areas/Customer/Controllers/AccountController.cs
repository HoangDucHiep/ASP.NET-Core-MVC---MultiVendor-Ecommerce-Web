using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.Account;
using MVEcommerce.Utility;
using System.Security.Claims;
using System.Transactions;

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

            AccountBecomeVendorVM vm = new AccountBecomeVendorVM()
            {
                Vendor = new Vendor()
                { UserId = userId }
            };


            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BecomeVendor(AccountBecomeVendorVM vm)
        {
            if (ModelState.IsValid)
            {
                Vendor vendor = vm.Vendor;

                vendor.Status = VendorStatus.PENDING;
                vendor.CreatedAt = DateTime.Now;
                vendor.UpdatedAt = DateTime.Now;

                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        // add Vendor Role for the user
                        var user = await _userManager.FindByIdAsync(vendor.UserId);
                        var roleResult = await _userManager.AddToRoleAsync(user, ApplicationRole.VENDOR);

                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Failed to add role to user.");
                            return View(vm);
                        }

                        unitOfWork.Vendor.Add(vm.Vendor);
                        unitOfWork.Save();

                        transaction.Complete();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        ModelState.AddModelError("", "An error occurred while processing your request.");
                        return View(vm);
                    }
                }
            }

            return View(vm);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
