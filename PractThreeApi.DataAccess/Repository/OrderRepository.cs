using PractThreeApi.DataAccess.Data;
using PractThreeApi.DataAccess.Repository.IRepository;
using PractThreeApi.Models;
using PractThreeApi.Models.ViewModel;

namespace PractThreeApi.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void PostOrder(RequestOrderitem requestOrderitem)
        {
          //  _db.OrderItems.Where(x => x.ProductId == requestOrderitem.orderVMs.Select(y => y.PId));
        }

        public void Update(Order order)
        {
            _db.Update(order);
        }

    }
}
