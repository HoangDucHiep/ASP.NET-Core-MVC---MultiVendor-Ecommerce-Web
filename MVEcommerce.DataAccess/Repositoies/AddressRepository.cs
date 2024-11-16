using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _db;
        public AddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        
        public void Update(Address obj)
        {
            _db.Update(obj);
        }
    }
}