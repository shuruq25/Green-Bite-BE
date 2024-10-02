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

        // get all
        // add Pagination
        // [HttpGet]
        // public async Task<ActionResult<List<CouponReadDto>>> GetAll(
        //     [FromQuery] PaginationOptions paginationOptions
        // )
        // {
        //     var CouponList = await _couponService.GetAllAsync(paginationOptions);
        //     return Ok(CouponList);
        //}

        // get by id
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponReadDto>> GetById([FromRoute] Guid id)
        {
            var Coupon = await _couponService.GetByIdAsync(id);
            return Ok(Coupon);
        }
    }
}
