using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
	public interface IProductRepository : IRepository<Product>
	{

		public Product GetBySlug(string slug);
		public Product GetProductBySlug(string slug);
		void Update(Product product);
	}
}
