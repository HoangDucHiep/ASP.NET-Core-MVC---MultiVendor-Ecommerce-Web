using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
	public interface IProductRepository : IRepository<Product>
	{

		public Product GetBySlug(string slug);
		
		void Update(Product product);
	}
}
