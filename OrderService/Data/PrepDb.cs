using Microsoft.EntityFrameworkCore;
using OrderService.Model;

namespace OrderService.Data
{
    public static class PrepDb
    {
        public static void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("InMemoryOrderDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(GetOrders());
                    context.SaveChanges();
                };
            }
        }

        private static IEnumerable<Order> GetOrders()
        {

            var stocks = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    ProductName = "Kompot",
                    Amount = 10,
                    UserId = 1,
                    Status = Status.Paid,
                    UpdatedDate = DateTime.Now
                },
                new Order()
                {
                    Id = 2,
                    UserId = 2,
                    ProductName = "Silnik odrzutowy",
                    Amount = 100,
                    Status = Status.Paid,
                    UpdatedDate = DateTime.Now
                }
            };
            return stocks;
        }
    }
}
