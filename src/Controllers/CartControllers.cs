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

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<ActionResult<List<CartReadDto>>> GetCartById([FromRoute] Guid id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CartReadDto>> UpdateCart(Guid id, [FromBody] CartUpdateDto cartUpdateDto)
        {
            try
            {
                var updatedCart = await _cartService.UpdateOneAsync(id, cartUpdateDto);
                return Ok(updatedCart); // Return the updated cart
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Return Not Found if the cart doesn't exist
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return StatusCode(500, "An error occurred while updating the cart."); // Return internal server error
            }
        }


        // // Retrieve the updated cart details
        // var updatedCart = await _cartService.GetCartByIdAsync(id);

        // // Check if the cart was found
        // if (updatedCart == null)
        // {
        //     return NotFound(); // Return Not Found if the cart doesn't exist
        // }




    }
}
