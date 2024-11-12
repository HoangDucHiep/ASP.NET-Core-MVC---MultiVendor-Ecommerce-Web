using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Repositoies
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        private readonly ApplicationDbContext _db;
        public VendorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Vendor obj)
        {
            _db.Update(obj);
        }
    }
}