using UserService.Dtos;

namespace UserService.Http
{
    public interface IHttpStockClient
    {
        Task<IEnumerable<ProductReadDto>> GetAllProductsAsync();
        Task<IEnumerable<ProductReadDto>> GetProductsByCategoryAsync(string categoryName);
        Task<ProductReadDto> GetProductById(int productId);
    }
}
