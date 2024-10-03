using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<CouponReadDto>> CreateOne(
            [FromBody] CouponCreateDto createDto
        )
        {
            var CouponCreated = await _couponService.CreatOneAsync(createDto);
            return Created($"/api/v1/Coupons/{CouponCreated.CouponId}", CouponCreated);
        }

       

        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponReadDto>> GetById([FromRoute] Guid id)
        {
            var Coupon = await _couponService.GetByIdAsync(id);
            return Ok(Coupon);
        }

        
        // Get all 
        [HttpGet]
        public async Task<ActionResult> GetAllCoupons()
        {
            return Ok(await _couponService.GetAllAsync());
        }



// Update 
           [HttpPut("{id}")]
        public async Task<ActionResult<CouponReadDto>> UpdateCoupon(Guid id,[FromBody] CouponUpdateDto updateCoupon)
        {
            var isUpdated = await _couponService.UpdateOneAsync(id, updateCoupon);
            if (!isUpdated)
            {
                return NotFound();
            }
            var result = await _couponService.GetByIdAsync(id);
            return Ok(result);
        }

// Delete

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon([FromRoute] Guid id)
        {
            var deleted = await _couponService.DeleteOneAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
