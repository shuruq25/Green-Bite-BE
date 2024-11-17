using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Services;
using src.Utils;
using static src.DTO.CouponDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponsController(ICouponService service)
        {
            _couponService = service;
        }

        // POST: /api/v1/coupons
        // Create a new coupon (Admin only)
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CouponReadDto>> CreateOne(
            [FromBody] CouponCreateDto createDto
        )
        {
            var couponCreated = await _couponService.CreateOneAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = couponCreated.CouponId }, couponCreated);
        }

        // GET: /api/v1/coupons
        // Retrieve all coupons (Authenticated users)
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CouponReadDto>>> GetAllCoupons()
        {
            var coupons = await _couponService.GetAllAsync();
            return Ok(coupons);
        }

        // DELETE: /api/v1/coupons/{id}
        // Delete a coupon by ID (Admin only)
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteCoupon([FromRoute] Guid id)
        {
            var deleted = await _couponService.DeleteOneAsync(id);
            if (!deleted)
            {
                throw CustomException.NotFound();
            }
            return NoContent();
        }

        // GET: /api/v1/coupons/{id}
        // Retrieve a coupon by ID (Authenticated users)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CouponReadDto>> GetById([FromRoute] Guid id)
        {
            var coupon = await _couponService.GetByIdAsync(id);
            if (coupon == null)
            {
                throw CustomException.NotFound();
            }
            return Ok(coupon);
        }
    }
}

