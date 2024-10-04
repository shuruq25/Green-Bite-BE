using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Entity;
using src.Services.review;
using src.Utils;
using static src.DTO.ReviewDTO;

namespace src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        protected readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewReadDto>>> GetAll(
            [FromQuery] PaginationOptions paginationOptions)
        {
            var reviews = await _reviewService.GetAllAsync(paginationOptions);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReadDto>> GetById([FromRoute] Guid id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                throw CustomException.NotFound();
            }
            return Ok(review);
        }

        [HttpGet("/order{orderId}")]
        public async Task<ActionResult<List<ReviewReadDto>>> GetReviewsByOrderId([FromRoute] Guid orderId)
        {
            var reviews = await _reviewService.GetReviewsByOrderIdAsync(orderId);
            return Ok(reviews);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewReadDto>> CreateReview(
            [FromBody] ReviewCreateDto createDto
        )
        {
            var userIdClaim = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw CustomException.UnAuthorized();
            }
            var userGuid = Guid.Parse(userIdClaim.Value);
            var reivewCreated = await _reviewService.CreateOneAsync(createDto, userGuid);
            return Created($"api/v1/review/{reivewCreated.ReviewId}", reivewCreated);
        }

        [HttpDelete("{id}")]

        [Authorize]

        public async Task<ActionResult> DeleteReview(Guid id)
        {
            var isDeleted = await _reviewService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                throw CustomException.NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ReviewReadDto>> UpdateReview(
            Guid id,
            [FromBody] ReviewUpdateDto updateDto
        )
        {
            var isUpdated = await _reviewService.UpdateOneAsync(id, updateDto);
            if (!isUpdated)
            {
                throw CustomException.NotFound();
            }
            var updatedReview = await _reviewService.UpdateOneAsync(id, updateDto);
            return Ok(updatedReview);
        }
    }
}
