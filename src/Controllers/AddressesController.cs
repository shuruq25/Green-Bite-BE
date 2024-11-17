using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Services;
using src.Services.UserService;
using src.Utils;
using static src.DTO.AddressDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AddressesController : ControllerBase
    {
        protected readonly IAddressService _addressService;
        protected  readonly IUserService _userService;

        public AddressesController(IAddressService service)
        {
            _addressService = service;
        }

        // POST: /api/v1/Addresses
        // Create a new address
        [HttpPost]
        [Authorize] 
        public async Task<ActionResult<AddressReadDto>> CreateOne(
            [FromBody] AddressCreateDto createDto, [FromRoute] Guid userId
        )
        {
            var addressCreated = await _addressService.CreatOneAsync(createDto);
            userId=addressCreated.UserId;
            return Created($"/api/v1/Addresses/{addressCreated.AddressId}", addressCreated);
        }

        // GET: /api/v1/Addresses
        // Get all addresses
        [HttpGet]
        [Authorize]  
        public async Task<ActionResult> GetAllAddresses()
        {
            return Ok(await _addressService.GetAllAsync());
        }

        // PUT: /api/v1/Addresses/{id}
        // Update an existing address
        [HttpPut("{id}")]
        [Authorize]  
        public async Task<ActionResult> UpdateAddress(
            Guid id,
            [FromBody] AddressUpdateDto updatedAddress
        )
        {
            if (await _addressService.UpdateOneAsync(id, updatedAddress))
            {
                return NoContent(); 
            }
            return NotFound();  
        }

        // DELETE: /api/v1/Addresses/{id}
        // Delete an address by its ID
        [HttpDelete("{id}")]
        [Authorize]  
        
         public async Task<IActionResult> DeleteAddress([FromRoute] Guid id)
        {
            var deleted = await _addressService.DeleteOneAsync(id);
            if (!deleted)
            {
                throw CustomException.NotFound(); 
            }
            return NoContent();  
        }

        // GET: /api/v1/Addresses/{id}
        // Get a specific address by its ID
        [HttpGet("{id}")]
        [Authorize]  
        public async Task<ActionResult<AddressReadDto>> GetById([FromRoute] Guid id)
        {
            var address = await _addressService.GetByIdAsync(id);
            return Ok(address);  
        }
    }
}
