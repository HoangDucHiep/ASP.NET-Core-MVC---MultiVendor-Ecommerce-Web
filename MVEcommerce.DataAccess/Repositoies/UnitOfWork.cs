using Microsoft.EntityFrameworkCore.Storage;
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
        public IProductImageRepository ProductImage { get; private set; }
        public IProductVariantRepository ProductVariant { get; private set; }
        public IProductVariantOptionRepository ProductVariantOption { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }


        public UnitOfWork (ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Vendor = new VendorRepository(_db);
            ProductImage = new ProductImageRepository(_db);
            ProductVariant = new ProductVariantRepository(_db);
            ProductVariantOption = new ProductVariantOptionRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Order = new OrderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }
    }
}