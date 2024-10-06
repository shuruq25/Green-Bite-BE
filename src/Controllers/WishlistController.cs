using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using src.Services;
using src.Utils;
using static src.DTO.WishlistDTO;

namespace src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WishlistReadDto>>> GetAll()
        {
            var wishlistItems = await _wishlistService.GetAllAsync();
            return Ok(wishlistItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WishlistReadDto>> GetById(Guid id)
        {
            var wishlistItem = await _wishlistService.GetByIdAsync(id);
            if (wishlistItem == null)
            {
                throw CustomException.NotFound();
            }
            return Ok(wishlistItem);
        }

        [HttpPost]
        public async Task<ActionResult<WishlistReadDto>> CreateOne(
            [FromBody] WishlistCreateDto createDto
        )
        {
            var authenticateClaims = HttpContext.User;
            var userId = authenticateClaims
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!
                .Value;
            var userGuid = new Guid(userId);
            createDto.UserID = userGuid;
            var newWishlistItem = await _wishlistService.CreateOneAsync(createDto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = newWishlistItem.WishlistID },
                newWishlistItem
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            var isDeleted = await _wishlistService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                throw CustomException.NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WishlistReadDto>> UpdateOne(
            Guid id,
            [FromBody] WishlistUpdateDto updateDto
        )
        {
            var isUpdated = await _wishlistService.UpdateOneAsync(id, updateDto);
            if (!isUpdated)
            {
                throw CustomException.NotFound();
            }
            var updatedWishlistItem = await _wishlistService.GetByIdAsync(id);
            return Ok(updatedWishlistItem);
        }
    }
}
