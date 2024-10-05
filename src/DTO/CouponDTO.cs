namespace src.DTO
{
    public class CouponDTO
    {
        // Class to : create Coupon

        public class CouponCreateDto()
        {
            public string? Code { get; set; }
            public decimal DiscountPercentage { get; set; }
            public DateTime Expire { get; set; }
        }

        // Class to : get/read data

        public class CouponReadDto()
        {
            public Guid CouponId { get; set; }
            public string? Code { get; set; }
            public decimal? DiscountPercentage { get; set; }
            public DateTime Expire { get; set; }
        }

        // Class to : Update Coupon

        public class CouponUpdateDto()
        {
            public string? Code { get; set; }
            public decimal? DiscountPercentage { get; set; }
            public DateTime Expire { get; set; }
        }
    }
}
