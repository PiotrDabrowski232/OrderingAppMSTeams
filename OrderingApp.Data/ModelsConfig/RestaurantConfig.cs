using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingApp.Data.Models;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Shared.Extensions;

namespace OrderingApp.Data.ModelsConfig
{
    public class RestaurantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Dishes)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(x => x.RestaurantId);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(x => x.RestaurantId);

            var restaurants = new Restaurant[]
            {
                CreateRestaurant(1.ToGuid(), "Panda Express", RestauranType.Chinese, 50.0f, 25.5f),
                CreateRestaurant(2.ToGuid(), "Joe's Pizza", RestauranType.Italian, 30.0f, 40.5f),
                CreateRestaurant(3.ToGuid(), "Burger Shack", RestauranType.FastFood, 25.5f, 20.5f),
                CreateRestaurant(4.ToGuid(), "Street Tacos", RestauranType.Street, 60.0f, 15.5f),
                CreateRestaurant(5.ToGuid(), "The Cozy Cafe", RestauranType.Cafe, 19.0f, 25.5f),
            };

            builder.HasData(restaurants);
        }

        private static Restaurant CreateRestaurant(Guid id, string name, RestauranType restaurantType, float freeDelivery, float PriceForDelivery)
        {
            return new Restaurant
            {
                Id = id,
                Name = name,
                RestaurantType = restaurantType,
                FreeDeliveryFrom = freeDelivery,
                PriceForDelivery = PriceForDelivery
            };
        }
    }
}