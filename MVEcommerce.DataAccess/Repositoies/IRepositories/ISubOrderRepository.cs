using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
	public interface ISubOrderRepository : IRepository<SubOrder>
	{
		void Update(SubOrder subOrder);
	}
}
