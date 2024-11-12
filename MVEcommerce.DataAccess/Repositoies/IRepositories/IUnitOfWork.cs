namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IVendorRepository Vendor { get; }
        void Save();
    }
}