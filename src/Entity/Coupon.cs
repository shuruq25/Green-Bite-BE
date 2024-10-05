namespace src.Entity
{
    public class Coupon
    {
        public Guid CouponId { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime Expire { get; set; }
    }
}
