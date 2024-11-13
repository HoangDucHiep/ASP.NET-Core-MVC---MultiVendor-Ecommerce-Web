using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class ProductVariantOptionRepository : Repository<ProductVariantOption>, IProductVariantOptionRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductVariantOptionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductVariantOption obj)
        {
            _db.Update(obj);
        }
    }
}