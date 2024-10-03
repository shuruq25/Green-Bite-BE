using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;
using static src.DTO.CartDTO;


namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        protected readonly ICartService _cartService;

        public CartController(ICartService service)
        {
            _cartService = service;
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
        //[Authorize]
        public async Task<ActionResult<CartReadDto>> CreateOneAsync([FromBody] CartCreateDto cartCreate)
        {
            var authenticateClaims = HttpContext.User;
            var userId = authenticateClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userGuid = new Guid(userId);
            return await _cartService.CreateOneAsync(userGuid, cartCreate);


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
                    return NotFound(new { message = $"Cart for User ID {userId} not found." });
                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the cart.", error = ex.Message });
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
                return NotFound();
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
        public async Task<ActionResult> Update(
                   [FromRoute] Guid id,
                   [FromBody] CartUpdateDto updateDto
               )
        {
            var result = await _cartService.UpdateOneAsync(id, updateDto);
            if (!result)
            {
                return NotFound();
            }
            var updatedProduct = await _cartService.GetCartByUserIdAsync(id);
            return Ok(updatedProduct);
        }


    }
}
