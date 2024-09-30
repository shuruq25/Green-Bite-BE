using System.Text.Json.Serialization;

namespace src.Entity
{
    public class Payment
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum PaymentMethod
        {
            CreditCard,
            PayPal,
            Cash,
            VISA,
            Other,
        }

 [JsonConverter(typeof(JsonStringEnumConverter))]
         public enum PaymentStatus
        {
            Pending,
            Completed,
            Failed,
            Refunded,
        }

        public Guid Id { get; set; }
        public decimal FinalPrice { get; set; }
        public PaymentMethod? Method { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public int CouponId { get; set; }
        public int OrderId { get; set; }
    }
}
