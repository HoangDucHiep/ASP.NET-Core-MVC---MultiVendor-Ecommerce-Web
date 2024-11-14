using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class SubOrderRepository : Repository<SubOrder>, ISubOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public SubOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        
        public void Update(SubOrder obj)
        {
            _db.Update(obj);
        }
    }
}