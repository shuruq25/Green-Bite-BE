using System.ComponentModel.DataAnnotations.Schema;
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
        public PaymentMethod? Method { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public Guid? CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public decimal FinalPrice => (Order.OriginalPrice) - (Order.OriginalPrice * (Coupon?.DiscountPercentage ?? 0));
    }
}
