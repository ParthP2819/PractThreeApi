namespace PractThreeApi.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        IProductRepository Product { get; }
        IOrderRepository Order { get; }
        IOrderItemRepository OrderItem { get; }
        void Save();

    }
}
