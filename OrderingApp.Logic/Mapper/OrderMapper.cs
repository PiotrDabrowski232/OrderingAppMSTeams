using AutoMapper;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId.ToString()));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => Guid.Parse(src.RestaurantId)));
        }
    }
}
