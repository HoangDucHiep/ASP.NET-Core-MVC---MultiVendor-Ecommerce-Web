namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        void Save();
    }
}