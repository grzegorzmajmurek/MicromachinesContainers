using AutoMapper;
using StockService.Dtos.Category;
using StockService.Dtos.Product;
using StockService.Dtos.Stock;
using StockService.Model;

namespace StockService.Profiles
{
    public class StocksProfile : Profile
    {
        public StocksProfile()
        {
            CreateMap<Stock, StockReadDto>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<Product, ProductReadDto>()
                .ForMember(x => x.CategoryName, r => r.MapFrom(p => p.Category.Name))
                .ForMember(x => x.StockName, r => r.MapFrom(p => p.Stock.Name));
        }
    }
}
