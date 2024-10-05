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
<<<<<<< HEAD
        public PaymentMethod? Method { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentStatus Status { get; set; }
        public Guid? CouponId { get; set; }
=======

        public PaymentMethod? Method { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentStatus Status { get; set; }

        public Guid CouponId { get; set; }
>>>>>>> 07441ef07784cbd7eddf28358892f20993330484
         public Coupon Coupon { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
