using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using System.Security.Claims;

namespace MVEcommerce.ViewComponents
{
	public class ProductCountViewComponent : ViewComponent
	{
		private readonly IShoppingCartRepository _shoppingCartRepository;

		public ProductCountViewComponent(IShoppingCartRepository shoppingCartRepository)
		{
			_shoppingCartRepository = shoppingCartRepository;
		}

		public IViewComponentResult Invoke()
		{
			int totalQuantity = 0;

			var user = HttpContext.User;

			if (user.Identity is { IsAuthenticated: true })
			{
				var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

				if (!string.IsNullOrEmpty(userId))
				{
					totalQuantity = _shoppingCartRepository
						.GetAll(c => c.UserId == userId)
						.Sum(c => c.Quantity);
				}
			}
			return View(totalQuantity);
		}
	}
}
