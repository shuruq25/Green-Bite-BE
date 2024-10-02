using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services;
using src.Utils;
using static src.DTO.CouponDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CouponsController : ControllerBase
    {
        protected readonly ICouponService _couponService;

        public CouponsController(ICouponService service)
        {
            _couponService = service;
        }

        //create

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CouponReadDto>> CreateOne(
            [FromBody] CouponCreateDto createDto
        )
        {
            var CouponCreated = await _couponService.CreatOneAsync(createDto);
            return Created($"/api/v1/Coupons/{CouponCreated.CouponId}", CouponCreated);
        }

        // Get all
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllCoupons()
        {
            return Ok(await _couponService.GetAllAsync());
        }

        // Update
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> UpdateCoupon(
            Guid id,
            [FromBody] CouponUpdateDto updatedCoupon
        )
        {
            if (await _couponService.UpdateOneAsync(id, updatedCoupon))
            {
                return NoContent();
            }
            return NotFound();
        }

        // Delete

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> DeleteCoupon(Guid id)
        {
            if (await _couponService.DeleteOneAsync(id))
            {
                return NoContent();
            }

            return NotFound();
        }

        // get by id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CouponReadDto>> GetById([FromRoute] Guid id)
        {
            var Coupon = await _couponService.GetByIdAsync(id);
            return Ok(Coupon);
        }
    }
}
