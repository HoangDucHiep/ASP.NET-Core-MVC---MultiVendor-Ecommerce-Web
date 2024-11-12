using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IVendorRepository Vendor { get; private set; }

        public UnitOfWork (ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Vendor = new VendorRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}