using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
	public interface IProductVariantRepository : IRepository<ProductVariant>
	{
		void Update(ProductVariant variant);
	}
}
