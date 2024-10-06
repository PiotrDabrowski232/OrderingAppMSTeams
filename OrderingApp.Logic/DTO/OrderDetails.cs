namespace OrderingApp.Logic.DTO
{
    public class OrderDetails
    {
        public string Name { get; set; }
        public float MinValue { get; set; }
        public float DeliveryCost { get; set; }
        public float FreeDeliveryFrom { get; set; }
        public DateTime CreationDate { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantType { get; set; }


    }
}
