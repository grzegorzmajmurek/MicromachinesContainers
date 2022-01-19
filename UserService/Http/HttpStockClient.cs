using System.Net.Http.Formatting;
using UserService.Dtos;

namespace UserService.Http
{
    public class HttpStockClient : IHttpStockClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://StockService:80";
        private readonly IConfiguration _configuration;

        public HttpStockClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsByCategoryAsync(string categoryName)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["StockService"]}category/{categoryName}");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<ProductReadDto>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["StockService"]}");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<ProductReadDto>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public async Task<ProductReadDto> GetProductById(int productId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["StockService"]}{productId}");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ProductReadDto>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }
    }
}
