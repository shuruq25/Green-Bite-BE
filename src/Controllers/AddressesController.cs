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

        // get all
        // add Pagination
        // The URL will be like : http://localhost:5125/api/v1/Addresses?offset=&limit=&search=

        // [HttpGet]
        // public async Task<ActionResult<List<AddressReadDto>>> GetAll(
        //     [FromQuery] PaginationOptions paginationOptions
        // )
        // {
        //     var AddressList = await _addressService.GetAllAsync(paginationOptions);
        //     return Ok(AddressList);
        // }

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
