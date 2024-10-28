using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Services;
using src.Utils;
using static src.DTO.AddressDTO;

namespace src.Controllers
{
    // Controller for managing addresses
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AddressesController : ControllerBase
    {
        // Address service injected via dependency injection
        protected readonly IAddressService _addressService;

        // Constructor to initialize the address service
        public AddressesController(IAddressService service)
        {
            _addressService = service;
        }

        // POST: /api/v1/Addresses
        // Create a new address
        [HttpPost]
        [Authorize]  // Only authenticated users can create an address
        public async Task<ActionResult<AddressReadDto>> CreateOne(
            [FromBody] AddressCreateDto createDto
        )
        {
            var addressCreated = await _addressService.CreatOneAsync(createDto);
            return Created($"/api/v1/Addresses/{addressCreated.AddressId}", addressCreated);
        }

        // GET: /api/v1/Addresses
        // Get all addresses
        [HttpGet]
        [Authorize]  // Only authenticated users can view addresses
        public async Task<ActionResult> GetAllAddresses()
        {
            return Ok(await _addressService.GetAllAsync());
        }

        // PUT: /api/v1/Addresses/{id}
        // Update an existing address
        [HttpPut("{id}")]
        [Authorize]  // Only authenticated users can update an address
        public async Task<ActionResult> UpdateAddress(
            Guid id,
            [FromBody] AddressUpdateDto updatedAddress
        )
        {
            if (await _addressService.UpdateOneAsync(id, updatedAddress))
            {
                return NoContent();  // Successfully updated
            }
            return NotFound();  // Address not found
        }

        // DELETE: /api/v1/Addresses/{id}
        // Delete an address by its ID
        [HttpDelete("{id}")]
        [Authorize]  // Only authenticated users can delete an address
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid id)
        {
            var deleted = await _addressService.DeleteOneAsync(id);
            if (!deleted)
            {
                throw CustomException.NotFound();  // Address not found
            }
            return NoContent();  // Successfully deleted
        }

        // GET: /api/v1/Addresses/{id}
        // Get a specific address by its ID
        [HttpGet("{id}")]
        [Authorize]  // Only authenticated users can view an address
        public async Task<ActionResult<AddressReadDto>> GetById([FromRoute] Guid id)
        {
            var address = await _addressService.GetByIdAsync(id);
            return Ok(address);  // Return the address details
        }
    }
}
