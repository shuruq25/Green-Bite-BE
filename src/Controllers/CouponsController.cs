using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CouponsController : ControllerBase
    {
        public static List<Coupon> Coupons = new List<Coupon>
        {
            new Coupon
            {
                CouponId = 1,
                Code = "DISCOUNT50",
                Discount_Percentage = 50,
                Expire = new DateTime(2024, 12, 31),
            },
            new Coupon
            {
                CouponId = 2,
                Code = "WELCOME10",
                Discount_Percentage = 10,
                Expire = new DateTime(2024, 10, 15),
            },
            new Coupon
            {
                CouponId = 3,
                Code = "SUMMER20",
                Discount_Percentage = 20,
                Expire = new DateTime(2024, 6, 30),
            },
        };

        // GET:
        [HttpGet]
        public ActionResult GetCoupons()
        {
            return Ok(Coupons);
        }

        // GET:
        [HttpGet("{id}")]
        public ActionResult GetCouponById(int id)
        {
            var foundCoupon = Coupons.FirstOrDefault(c => c.CouponId == id);
            if (foundCoupon == null)
            {
                return NotFound();
            }
            return Ok(foundCoupon);
        }

        // POST:
        [HttpPost]
        public ActionResult PostCoupon(Coupon newCoupon)
        {
            if (newCoupon == null || string.IsNullOrEmpty(newCoupon.Code))
            {
                return BadRequest("Coupon details are required.");
            }

            return Created($"api/v1/coupons/{newCoupon.CouponId}", newCoupon);
        }

        // PUT:
        [HttpPut("{id}")]
        public ActionResult PutCoupon(int id, Coupon updatedCoupon)
        {
            if (updatedCoupon == null || string.IsNullOrEmpty(updatedCoupon.Code))
            {
                return BadRequest("Coupon details are required.");
            }
            var foundCoupon = Coupons.FirstOrDefault(c => c.CouponId == id);
            if (foundCoupon == null)
            {
                return NotFound();
            }

            foundCoupon.Code = updatedCoupon.Code;

            return NoContent();
        }

        // DELETE:
        [HttpDelete("{id}")]
        public ActionResult DeleteCoupon(int id)
        {
            var foundCoupon = Coupons.FirstOrDefault(c => c.CouponId == id);
            if (foundCoupon == null)
            {
                return NotFound();
            }
            Coupons.Remove(foundCoupon);
            return NoContent();
        }
    }
}
