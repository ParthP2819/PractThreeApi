using PractThreeApi.DataAccess.Data;
using PractThreeApi.DataAccess.Repository.IRepository;
using PractThreeApi.Models;

namespace PractThreeApi.DataAccess.Repository
{
    public class OrderItemRepository :  Repository<OrderItems>, IOrderItemRepository
    {
         private readonly ApplicationDbContext _db;

        public OrderItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderItems item)
        {
            _db.Update(item);
        }
    }
}
