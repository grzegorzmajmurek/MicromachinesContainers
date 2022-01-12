using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockService.Dtos.Product;
using StockService.Repository.Interfaces;

namespace StockService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public StocksController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
        {
            var productEntities = await _productRepository.GetAll();

            return Ok(_mapper.Map<List<ProductReadDto>>(productEntities));
        }

        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProductsFromCategory(string categoryName)
        {
            var productEntities = await _productRepository.GetAllWithCondition(u => u.Category.Name == categoryName);

            if (!productEntities.Any())
                return NotFound();

            return Ok(_mapper.Map<List<ProductReadDto>>(productEntities));
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int productId)
        {
            var productEntity = await _productRepository.GetSingle(productId);

            if (productEntity == null)
                return NotFound();

            return Ok(_mapper.Map<ProductReadDto>(productEntity));
        }
    }
}
