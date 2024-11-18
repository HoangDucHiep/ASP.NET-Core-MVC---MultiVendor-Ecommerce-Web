using Microsoft.AspNetCore.Mvc;
using MVEcommerce.DataAccess.Repositoies.IRepositories;

namespace MVEcommerce.ViewComponents
{
    public class MobileCategoryItemViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public MobileCategoryItemViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categoryItem=_categoryRepository.GetAll();
            return View(categoryItem);
        }
    }


}
