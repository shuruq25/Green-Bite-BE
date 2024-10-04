using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
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
        protected readonly ICartService _cartService;
        protected readonly ILogger<LoggingMiddleware> _logger;

        public CartController(ICartService service, ILogger<LoggingMiddleware> logger)
        {
            _cartService = service;
            _logger = logger;
        }

        // POST: api/cart
        // This endpoint creates a new cart for a user
        // Example request body:
        // {
        //     "CartDetails": [
        //         { "ProductId": "", "Quantity": },
        //         { "ProductId": "", "Quantity":  }
        //     ]
        // }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartReadDto>> CreateOneAsync([FromBody] CartCreateDto cartCreate)
        {
            try
            {
                var authenticateClaims = HttpContext.User;
                var userId = authenticateClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
                if (userId == null)
                {
                    throw CustomException.UnAuthorized("User is not authenticated.");
                }
                var userGuid = new Guid(userId);
            
                return await _cartService.CreateOneAsync(userGuid, cartCreate);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, $"An error occurred while creating the cart for User ID");

                throw CustomException.InternalError("An error occurred while creating the cart.");
            }


        }

        // GET: api/cart/user/{userId}
        // This endpoint retrieves the cart for a specific user by their UserId
        // Example:
        // GET /api/cart/user/{userId}

        [HttpGet("user/{userId}")]
        // [Authorize]
        public async Task<IActionResult> GetCartByUserId(Guid userId)
        {
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(userId);

                if (cart == null)
                {
                    throw CustomException.NotFound($"Cart for User ID {userId} not found.");
                }

                return Ok(cart);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the cart for User ID {userId}.", userId);

                throw CustomException.InternalError("An error occurred while retrieving the cart.");
            }
        }
        // DELETE: api/cart/{id}
        // This endpoint deletes a cart based on the provided userId
        // Example:
        // DELETE /api/cart/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid id)
        {
            var deleted = await _cartService.DeleteCartAsync(id);

            if (!deleted)
            {
                throw CustomException.NotFound($"Cart with ID {id} not found.");
            }

            return NoContent();
        }

        // PUT: api/cart/{id}
        // This endpoint updates the cart for the specified userId with new details
        // Example request body:
        // {
        //     "CartDetails": [
        //         { "ProductId": "", "Quantity":  },
        //         { "ProductId": "", "Quantity": }
        //     ]
        // }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] CartUpdateDto updateDto)
        {
            var result = await _cartService.UpdateOneAsync(id, updateDto);
            if (!result)
            {
                throw CustomException.NotFound($"Cart with ID {id} not found.");
            }
            var updatedProduct = await _cartService.GetCartByUserIdAsync(id);
            return Ok(updatedProduct);
        }


    }
}
