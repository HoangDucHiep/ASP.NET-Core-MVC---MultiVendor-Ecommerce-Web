using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class ProductVariantRepository : Repository<ProductVariant>, IProductVariantRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductVariantRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductVariant obj)
        {
            _db.Update(obj);
        }
    }
}