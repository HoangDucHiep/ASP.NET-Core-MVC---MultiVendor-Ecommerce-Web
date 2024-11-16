using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
	public interface IProductRepository : IRepository<Product>
	{
		void Update(Product product);
		public Product GetProductBySlug(string slug);
	}
}
