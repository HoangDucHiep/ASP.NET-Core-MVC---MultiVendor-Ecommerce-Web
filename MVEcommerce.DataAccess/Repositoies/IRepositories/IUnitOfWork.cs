using Microsoft.EntityFrameworkCore.Storage;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IVendorRepository Vendor { get; }
        IProductImageRepository ProductImage { get; }
        IProductVariantRepository ProductVariant { get; }
        IProductVariantOptionRepository ProductVariantOption { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderRepository Order { get; }
        IOrderDetailRepository OrderDetail { get; }
        ISubOrderRepository SubOrder { get; }


        void Save();

        IDbContextTransaction BeginTransaction();
    }
}