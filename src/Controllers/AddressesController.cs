using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;
using static src.DTO.AddressDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AdressesController : ControllerBase
    {
        protected readonly IAddressService _addressService;

        //DI logic of  the Services.Address in AdressesController
        public AdressesController(IAddressService service)
        {
            _addressService = service;
        }

        //create

        [HttpPost]
        public async Task<ActionResult<AddressReadDto>> CreateOne(AddressCreateDto createDto)
        {
            var addressCreated = await _addressService.CreatOneAsync(createDto);

            // return Created(Url ,addressCreated)
            return Ok(addressCreated);
        }


    }
}
