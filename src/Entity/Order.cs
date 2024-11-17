using System.Text.Json.Serialization;

namespace src.Entity
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatuses
    {
        Pending,
        Shipped,
        Delivered,
    }

    public class Order
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public decimal OriginalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime EstimatedArrival { get; set; } = DateTime.UtcNow.AddDays(10);
        public OrderStatuses Status { get; set; } = OrderStatuses.Pending;
        public Guid? PaymentID { get; set; }
        public Payment? Payment { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}
