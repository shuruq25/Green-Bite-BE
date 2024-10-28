using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Middlewares;
using src.Services;
using src.Utils;
using static src.DTO.CartDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        // Cart service and logger injected via dependency injection
        protected readonly ICartService _cartService;
        protected readonly ILogger<LoggingMiddleware> _logger;

        public CartController(ICartService service, ILogger<LoggingMiddleware> logger)
        {
            _cartService = service;
            _logger = logger;
        }

        // POST: /api/cart
        // Create a new cart for an authenticated user
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartReadDto>> CreateOneAsync([FromBody] CartCreateDto cartCreate)
        {
            try
            {
                // Get the authenticated user's ID from claims
                var userId = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    throw CustomException.UnAuthorized("User is not authenticated.");
                }
                var userGuid = new Guid(userId);

                // Call the service to create a cart
                return await _cartService.CreateOneAsync(userGuid, cartCreate);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error creating the cart.");
                throw CustomException.InternalError("An error occurred while creating the cart.");
            }
        }

        // GET: /api/cart/user/{userId}
        // Retrieve the cart for a specific user by their UserId
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetCartByUserId(Guid userId)
        {
            try
            {
                // Call the service to retrieve the cart
                var cart = await _cartService.GetCartByUserIdAsync(userId);

                if (cart == null)
                {
                    throw CustomException.NotFound($"Cart for User ID {userId} not found.");
                }

                return Ok(cart);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, $"Error retrieving the cart for User ID {userId}.");
                throw CustomException.InternalError("An error occurred while retrieving the cart.");
            }
        }

        // DELETE: /api/cart/{id}
        // Delete a cart by its ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid id)
        {
            var deleted = await _cartService.DeleteCartAsync(id);

            if (!deleted)
            {
                throw CustomException.NotFound($"Cart with ID {id} not found.");
            }

            return NoContent(); // Return 204 No Content on successful deletion
        }

        // PUT: /api/cart/{id}
        // Update the cart for a specific user
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] CartUpdateDto updateDto
        )
        {
            var result = await _cartService.UpdateOneAsync(id, updateDto);

            if (!result)
            {
                throw CustomException.NotFound($"Cart with ID {id} not found.");
            }

            // Return the updated cart details
            var updatedCart = await _cartService.GetCartByUserIdAsync(id);
            return Ok(updatedCart);
        }
    }
}

