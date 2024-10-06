using AutoMapper;
using OrderingApp.Data.Models;
using OrderingApp.Data.Models.Enum;
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
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => Guid.Parse(src.RestaurantId)))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));


            CreateMap<Order, OrderDetails>()
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.RestaurantType, opt => opt.MapFrom(src => Enum.GetName(typeof(RestauranType), src.Restaurant.RestaurantType)));

        }
    }
}
