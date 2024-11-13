namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IVendorRepository Vendor { get; }
        IProductImageRepository ProductImage { get; }
        IProductVariantRepository ProductVariant { get; }
        IProductVariantOptionRepository ProductVariantOption { get; }
        
        void Save();
    }
}