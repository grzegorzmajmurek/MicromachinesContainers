using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Model;

namespace OrderService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            await _db.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await Task.Run(() => _db.Orders);
        }

        public async Task<IEnumerable<Order>> GetAllByCondition(Func<Order, bool> condition)
        {
            return await Task.Run(() => _db.Orders.Where(condition));
        }

        public async Task<Order> GetSingle(int orderId)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task Update(Order order)
        {
            await Task.Run(() => _db.Orders.Update(order));
        }
    }
}
