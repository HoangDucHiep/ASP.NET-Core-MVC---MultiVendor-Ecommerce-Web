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
            // if current user is already a vendor, redirect to INdex()
            if (User.IsInRole(ApplicationRole.VENDOR))
            {
                return RedirectToRoute(new { area = "VendorArea", controller = "Dashboard", action = "Index" });
            }

            if (User.Identity is not ClaimsIdentity claimsIdentity)
            {
                return RedirectToAction("Error", "Home");
            }
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var userId = userIdClaim.Value;

        
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
                MVEcommerce.Models.Vendor vendor = vm.Vendor;
        
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

                        // Add vendor status to claims
                        var claims = new List<Claim>
                        {
                            new Claim("VendorStatus", vendor.Status)
                        };
                        await _userManager.AddClaimsAsync(user, claims);
        
                        // add Vendor to database
                        vm.Vendor.Status = VendorStatus.PENDING;
                        unitOfWork.Vendor.Add(vm.Vendor);
                        unitOfWork.Save();
                        await _signInManager.SignInAsync(user, isPersistent: false);
        
                        transaction.Complete();
                        return RedirectToRoute(new { area = "VendorArea", controller = "Dashboard", action = "Index" });
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        ModelState.AddModelError("", "An error occurred while processing your request.");
                        return View(vm);
                    }
                }
            }
        
            return RedirectToAction();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "The new password and confirmation password do not match.");
                return RedirectToAction("AccountDetail");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return RedirectToAction("AccountDetail");
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("AccountDetail");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("AccountDetail");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
