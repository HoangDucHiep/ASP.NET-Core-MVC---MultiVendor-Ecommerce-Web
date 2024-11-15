using Microsoft.EntityFrameworkCore;
using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

		public Product GetBySlug(string slug)
		{
			return _db.Products.FirstOrDefault(c => c.Slug == slug);
		}
		public Product GetProductBySlug(string slug)
		{
			return _db.Products
				.Include(p => p.ProductImages)
				.Include(p => p.ProductVariants)
					.ThenInclude(v => v.ProductVariantOptions)
					.Include(p => p.Category)
				.FirstOrDefault(p => p.Slug == slug);
		}
		public void Update(Product obj)
        {
            _db.Update(obj);
        }
    }
}