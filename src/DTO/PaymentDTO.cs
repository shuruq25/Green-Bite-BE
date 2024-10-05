using src.Entity;
using static src.Entity.Payment;

namespace src.DTO
{
    public class PaymentDTO
    {
        public class PaymentCreateDto
        {
            public decimal FinalPrice { get; set; }
            public PaymentMethod? Method { get; set; }
            public Guid? CouponId { get; set; }
            public Guid OrderId { get; set; }
        }
        public class PaymentReadDto
        {
            public Guid Id { get; set; }
            public decimal FinalPrice { get; set; }
            public PaymentMethod? Method { get; set; }
            public DateTime PaymentDate { get; set; }
            public PaymentStatus Status { get; set; }
            public Guid CouponId { get; set; }
            public Guid OrderId { get; set; }
        }

        public class PaymentUpdateDto
        {
            public decimal FinalPrice { get; set; }
            public PaymentMethod? Method { get; set; }
            public DateTime PaymentDate { get; set; }
            public PaymentStatus Status { get; set; }
            public Guid CouponId { get; set; }
            public Guid OrderId { get; set; }
        }
    }
}
