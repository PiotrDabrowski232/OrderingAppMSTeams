using OrderingApp.Data.Models.Enum;

namespace OrderingApp.Data.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RestauranType RestaurantType { get; set; }
        public float FreeDeliveryFrom {  get; set; }
        public float PriceForDelivery {  get; set; }


        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
