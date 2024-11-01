using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Services.UserService;
using src.Utils;
using static src.DTO.UserDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserReadDto>> UserSignUp([FromBody] UserCreateDto createDto)
        {
            var userCreated = await _userService.CreateOneAsync(createDto);
            return Created($"api/v1/user/{userCreated.UserID}", userCreated);
        }
        
        [HttpPost("signin")]
        public async Task<ActionResult<string>> SignInUser([FromBody] UserSignInDto signInDtoDto)
        {
            var token = await _userService.SignInAsync(signInDtoDto);
            return Ok(token);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<List<UserReadDto>>> GetAll(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var userList = await _userService.GetAllAsync(paginationOptions);
            return Ok(userList);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<UserReadDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                throw CustomException.NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            var isDeleted = await _userService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                throw CustomException.NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserReadDto>> UpdateOne(
            Guid id,
            [FromBody] UserUpdateDto updateDto
        )
        {
            var isUpdated = await _userService.UpdateOneAsync(id, updateDto);
            if (!isUpdated)
            {
                return NotFound(new { Message = "User not found for update" });
            }
            var updatedUser = await _userService.GetByIdAsync(id);
            return Ok(updatedUser);
        }

    }
}
