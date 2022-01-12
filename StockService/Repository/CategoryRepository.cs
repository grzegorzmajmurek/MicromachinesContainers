using Microsoft.EntityFrameworkCore;
using StockService.Data;
using StockService.Model;
using StockService.Repository.Interfaces;

namespace StockService.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _db.Categories.Include(x => x.Products).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllWithCondition(Func<Category, bool> condition)
        {
            return await Task.Run(() => _db.Categories.
            Include(x => x.Products).
            Where(condition).ToList());
        }

        public async Task<Category> GetSingle(int id)
        {
            return await Task.Run(() => _db.Categories
            .Include(x => x.Products)
            .FirstOrDefault(c => c.Id == id));
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
