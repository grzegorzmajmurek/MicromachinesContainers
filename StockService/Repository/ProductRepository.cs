using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Model;
using StockService.Repository.Interfaces;

namespace StockService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products
            .Include(p => p.Stock)
            .Include(p => p.Category)
            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithCondition(Func<Product, bool> condition)
        {
            return await Task.Run(() => _db.Products
            .Include(p => p.Stock)
            .Include(p => p.Category)
            .Where(condition)
            .ToList());
        }

        public async Task<Product> GetSingle(int id)
        {
            return await _db.Products
            .Include(p => p.Stock)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
