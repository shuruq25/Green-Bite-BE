using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponsController : ControllerBase { 

        public static List<Coupon> coupons = new List<Coupon> { 
            // new Coupon {CouponId= , Code="", Discount_Percentage = , Expire=}
    };
}}
