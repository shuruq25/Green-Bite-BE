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



    }
}
