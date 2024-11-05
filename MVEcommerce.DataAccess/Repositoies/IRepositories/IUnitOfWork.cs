namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        void Save();
    }
}