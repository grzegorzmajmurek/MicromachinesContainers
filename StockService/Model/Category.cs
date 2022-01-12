using StockService.Model.Interfaces;

namespace StockService.Model
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
