using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category category);

		IEnumerable<Category> GetAllCategory();
	}
}
