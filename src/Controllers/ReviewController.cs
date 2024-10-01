using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            [FromQuery] PaginationOptions paginationOptions
        )
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
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewReadDto>> CreateReview(
            [FromBody] ReviewCreateDto createDto
        )
        {
            var reivewCreated = await _reviewService.CreateOneAsync(createDto);
            return Created($"api/v1/review/{reivewCreated.ReviewId}", reivewCreated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(Guid id)
        {
            var isDeleted = await _reviewService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewReadDto>> UpdateReview(
            Guid id,
            [FromBody] ReviewUpdateDto updateDto
        )
        {
            var isUpdated = await _reviewService.UpdateOneAsync(id, updateDto);
            if (!isUpdated)
            {
                return NotFound();
            }
            var updatedreview = await _reviewService.GetByIdAsync(id);
            return Ok(updatedreview);
        }
    }
}
