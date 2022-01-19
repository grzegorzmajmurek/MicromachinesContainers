using System.Net.Http.Formatting;
using UserService.Dtos;

namespace UserService.Http
{
    public class HttpOrdersClient : IHttpOrdersClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpOrdersClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<OrderReadDto> CreateOrder(OrderCreateDto orderDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_configuration["OrderService"]}", orderDto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<OrderReadDto>(new[] { new JsonMediaTypeFormatter() });
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["OrderService"]}");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<OrderReadDto>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderReadDto>> GetUsersOrderHistoryAsync(int userId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["OrderService"]}{userId}");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<OrderReadDto>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderReadDto>> GetUsersPurchaseHistoryAsync(int userId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["OrderService"]}purchases/{userId}");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<OrderReadDto>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public async Task<OrderReadDto> PayForOrder(int orderId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"{_configuration["OrderService"]}{orderId}");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<OrderReadDto>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }
    }
}
