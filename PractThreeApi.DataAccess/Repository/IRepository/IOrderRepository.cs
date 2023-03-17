

using PractThreeApi.Models;
using PractThreeApi.Models.ViewModel;

namespace PractThreeApi.DataAccess.Repository.IRepository
{ 
    public interface IOrderRepository : IRepository<Order>
    {
        public void Update(Order order);

        public void PostOrder(RequestOrderitem requestOrderitem);
    }
}
