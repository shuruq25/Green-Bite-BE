using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.Entity;
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

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateOne([FromBody] UserCreateDto createDto)
        {
            var userCreated = await _userService.CreateOneAsync(createDto);
            return Created($"api/v1/user/{userCreated.UserID}", userCreated);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserReadDto>>> GetAll(
            [FromQuery] PaginationOptions paginationOptions
        )
        {
            var userList = await _userService.GetAllAsync(paginationOptions);
            return Ok(userList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            var isDeleted = await _userService.DeleteOneAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
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

        // [HttpPost("signup")]
        // public ActionResult SignUpUser([FromBody] User user)
        // {
        //     PasswordUtils.HashPassword(user.Password, out string hashedPass, out byte[] salt);
        //     user.Password = hashedPass;
        //     user.Salt = salt;
        //     users.Add(user);
        //     return Created($"/api/v1/user/{user.UserID}", user);
        // }

        // [HttpPost("login")]
        // public ActionResult LogIn(User user)
        // {
        //     User? foundUser = users.FirstOrDefault(p => p.EmailAddress == user.EmailAddress);
        //     if (foundUser == null)
        //     {
        //         return NotFound();
        //     }

        //     bool isMatched = PasswordUtils.VerifyPassword(
        //         user.Password,
        //         foundUser.Password,
        //         foundUser.Salt
        //     );
        //     if (!isMatched)
        //     {
        //         return Unauthorized();
        //     }
        //     return Ok(foundUser);
        // }
    }
}
