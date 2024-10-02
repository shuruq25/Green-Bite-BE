using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;
using src.Utils;
using static src.DTO.AddressDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AddressesController : ControllerBase
    {
        protected readonly IAddressService _addressService;

        //DI logic of  the Services.Address in AdressesController
        public AddressesController(IAddressService service)
        {
            _addressService = service;
        }

        //create

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AddressReadDto>> CreateOne(
            [FromBody] AddressCreateDto createDto
        )
        {
            var addressCreated = await _addressService.CreatOneAsync(createDto);
            return Created($"/api/v1/Addresses/{addressCreated.AddressId}", addressCreated);
        }

        // Get all
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllAddresses()
        {
            return Ok(await _addressService.GetAllAsync());
        }

        // Update
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

        // Delete

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAddress(Guid id)
        {
            if (await _addressService.DeleteOneAsync(id))
            {
                return NoContent();
            }

            return NotFound();
        }

        // get by id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AddressReadDto>> GetById([FromRoute] Guid id)
        {
            var Address = await _addressService.GetByIdAsync(id);
            return Ok(Address);
        }
    }
}
