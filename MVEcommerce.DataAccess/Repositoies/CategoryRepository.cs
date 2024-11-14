using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

		public Category GetBySlug(string slug)
		{
			return _db.Categories.FirstOrDefault(c => c.Slug == slug);
		}


		public void Update(Category obj)
        {
            _db.Update(obj);
        }
    }
}