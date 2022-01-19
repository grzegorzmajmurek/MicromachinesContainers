using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
using UserService.Http;
using UserService.Repository;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpOrdersClient _ordersClient;
        private readonly IHttpStockClient _stockClient;

        public UsersController(IHttpOrdersClient ordersClient,
                               IHttpStockClient stockClient, 
                               IMapper mapper, 
                               IUserRepository userRepository)
        {
            _ordersClient = ordersClient;
            _stockClient = stockClient;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllOrders()
        {
            var orders = await _ordersClient.GetAllOrdersAsync();
            if (orders == null) return BadRequest();
            return Ok(orders);
        }

        [HttpGet("orders/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetUsersOrderHistory(int userId)
        {
            var orders = await _ordersClient.GetUsersOrderHistoryAsync(userId);
            if (orders == null) return BadRequest();
            return Ok(orders);
        }

        [HttpGet("purchases/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetUsersPurchaseHistory(int userId)
        {
            var orders = await _ordersClient.GetUsersPurchaseHistoryAsync(userId);
            return Ok(orders);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> BrowseProducts()
        {
            var products = await _stockClient.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("products/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> BrowseProductsByCategory(string categoryName)
        {
            var products = await _stockClient.GetProductsByCategoryAsync(categoryName);
            return Ok(products);
        }

        [HttpPost("orders/{userId}")]
        public async Task<ActionResult<OrderReadDto>> CreateNewOrder(int productId, int userId)
        {
            var product = await _stockClient.GetProductById(productId);
            var user = await _userRepository.GetSingle(userId);
            var newOrderDto = new OrderCreateDto() { UserId = userId, ProductName = product.Name, Amount = product.Price };

            if (!product.Available)
            {
                return NotFound("The item you are trying to purchase is unavailable.");
            }
            else if (user.Funds < product.Price)
            {
                return BadRequest("You have insufficient funds.");
            }

            var newOrder = await _ordersClient.CreateOrder(newOrderDto);

            return Ok(newOrder);
        }

        [HttpPut("orders/{orderId}")]
        public async Task<ActionResult<OrderReadDto>> PayForOrder(int orderId)
        {
            var order = await _ordersClient.PayForOrder(orderId);

            if (order == null) return BadRequest("cannot update");

            return Ok(order);
        }
    }
}
