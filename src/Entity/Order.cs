using System.Text.Json.Serialization;
using src.DTO;

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
        public DateTime EstimatedArrival { get; set; }
        public OrderStatuses Status { get; set; }
        public Guid? PaymentID { get; set; }
        public Payment? Payment { get; set; }
    }
}
