using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string? Code { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime Expire { get; set; }
    }
}
