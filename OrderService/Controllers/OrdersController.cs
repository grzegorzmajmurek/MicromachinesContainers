using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Dtos;
using OrderService.Model;
using OrderService.Repository;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllOrdersHistory()
        {
            var orders = await _orderRepository.GetAll();

            return Ok(_mapper.Map<List<OrderReadDto>>(orders));
        }

        [HttpGet("id/{orderId}")]
        public async Task<ActionResult<OrderReadDto>> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetSingle(orderId);

            return Ok(_mapper.Map<OrderReadDto>(order));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetUsersOrderHistory(int userId)
        {
            var orders = await _orderRepository.GetAllByCondition(o => o.UserId == userId);

            return Ok(_mapper.Map<List<OrderReadDto>>(orders));
        }

        [HttpGet("purchases/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetUsersPurchaseHistory(int userId)
        {
            var orders = await _orderRepository.GetAllByCondition(o => o.UserId == userId && o.Status == Status.Paid);

            return Ok(_mapper.Map<List<OrderReadDto>>(orders));
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOrder(OrderCreateDto order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            await _orderRepository.Create(orderEntity);
            await _orderRepository.Save();

            return Ok(_mapper.Map<OrderReadDto>(orderEntity));
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderReadDto>> PayForOrder(int orderId)
        {
            var order = await _orderRepository.GetSingle(orderId);

            order.Status = Status.Paid;
            await _orderRepository.Save();

            return Ok(_mapper.Map<OrderReadDto>(order));
        }
    }
}
