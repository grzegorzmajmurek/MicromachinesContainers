using Microsoft.EntityFrameworkCore;
using StockService.Model;

namespace StockService.Data
{
    public static class PrepDb
    {
        public static void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryStockDb")
            .Options;

            using (var context = new AppDbContext(options))
            {
                if (!context.Stocks.Any())
                {
                    context.Stocks.AddRange(GetStocks());
                    context.SaveChanges();
                }
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(GetCategories());
                    context.SaveChanges();
                }
                if (!context.Products.Any())
                {
                    context.Products.AddRange(GetProducts());
                    context.SaveChanges();
                }
            }
        }

        private static IEnumerable<Stock> GetStocks()
        {
            var stocks = new List<Stock>()
            {
                new Stock(){ Id = 1, Name = "Magazyn części" },
                new Stock(){ Id = 2, Name = "Magazyn jedzenia" }
            };
            return stocks;
        }

        private static IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category(){ Id = 1, Name = "Silnik" },
                new Category(){ Id = 2, Name = "Nabiał" },
                new Category(){ Id = 3, Name = "Pieczywo" },
            };
            return categories;
        }

        private static IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product(){ Id = 1, Name = "Mleko", StockId = 2, CategoryId = 2, Price = 2.55M },
                new Product(){ Id = 2, Name = "Maślanka", StockId = 2, CategoryId = 2, Price = 8.85M },
                new Product(){ Id = 3, Name = "Bułka", StockId = 2, CategoryId = 3, Price = 10.00M },
                new Product(){ Id = 4, Name = "Silnik spalinowy", StockId = 1, CategoryId = 2, Price = 1.50M },
                new Product(){ Id = 5, Name = "Silnik elektryczny", StockId = 1, CategoryId = 1, Price = 398.00M },
                new Product(){ Id = 6, Name = "Silnik hybrydowy", StockId = 1, CategoryId = 1, Price = 0.90M },
            };
            return products;
        }
    }
}
