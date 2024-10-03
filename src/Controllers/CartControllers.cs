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
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartReadDto>> CreateOneAsync([FromBody] CartCreateDto cartCreate)
        {
            var authenticateClaims = HttpContext.User;
            var userId = authenticateClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var userGuid = new Guid(userId);
            return await _cartService.CreateOneAsync(userGuid, cartCreate);


        }
        // GET: api/cart/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize]
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid id)
        {
            var deleted = await _cartService.DeleteCartAsync(id);

            if (!deleted)
            {
                return NotFound(); // Cart not found
            }

            return NoContent(); // Cart deleted successfully
        }



    }
}
