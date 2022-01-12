using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Model;
using StockService.Repository.Interfaces;

namespace StockService.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _db;
        public StockRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Stock>> GetAll()
        {
            return await _db.Stocks.ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetAllWithCondition(Func<Stock, bool> condition)
        {
            return await Task.Run(() => _db.Stocks.Where(condition));
        }

        public async Task<Stock> GetSingle(int id)
        {
            return await Task.Run(() => _db
            .Stocks.Include(s => s.Products)
            .FirstOrDefault(s => s.Id == id));
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
