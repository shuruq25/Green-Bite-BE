namespace src.Entity
{
    public class Coupon
    {
        public Guid CouponId { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime Expire { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
