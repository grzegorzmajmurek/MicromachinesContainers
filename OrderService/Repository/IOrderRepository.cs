using OrderService.Model;

namespace OrderService.Repository
{
    public interface IOrderRepository
    {
        Task Create(Order order);
        Task Save();
        Task Update(Order order);
        Task<IEnumerable<Order>> GetAllByCondition(Func<Order, bool> condition);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetSingle(int orderId);
    }
}
