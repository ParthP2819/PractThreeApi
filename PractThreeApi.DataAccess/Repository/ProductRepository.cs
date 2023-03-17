using PractThreeApi.DataAccess.Data;
using PractThreeApi.DataAccess.Repository.IRepository;
using PractThreeApi.Models;

namespace PractThreeApi.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Product> Add()
        {
            throw new NotImplementedException();
        }
    }
}
