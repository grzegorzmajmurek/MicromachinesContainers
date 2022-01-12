using StockService.Model.Interfaces;

namespace StockService.Model
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockId { get; set; }
        public Stock? Stock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; } = true;
    }
}
