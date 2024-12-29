using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Services;
using static src.DTO.SubscriptionDTO;

namespace src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // POST: api/subscription
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<SubscriptionReadDto>> CreateSubscriptionAsync([FromBody] SubscriptionCreateDto createDto)
        {
            try
            {
                var result = await _subscriptionService.CreateSubscriptionAsync(createDto);
                return Created($"/api/v1//{result.ID}", result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/subscription
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<List<SubscriptionReadDto>>> GetAllSubscriptionsAsync()
        {
            try
            {
                var result = await _subscriptionService.GetAllSubscriptionsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/subscription/{id}
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<SubscriptionReadDto>> GetSubscriptionByIdAsync(Guid id)
        {
            try
            {
                var result = await _subscriptionService.GetSubscriptionByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = "Subscription not found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/subscription/{id}
        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult> UpdateSubscriptionAsync(Guid id, [FromBody] SubscriptionUpdateDto updateDto)
        {
            try
            {
                var result = await _subscriptionService.UpdateSubscriptionAsync(id, updateDto);
                if (!result)
                {
                    return NotFound(new { message = "Subscription not found." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/subscription/{id}
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult> DeleteSubscriptionAsync(Guid id)
        {
            try
            {
                var result = await _subscriptionService.DeleteSubscriptionAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Subscription not found." });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
