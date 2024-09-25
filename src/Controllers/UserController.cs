using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.Entity;

namespace src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        public static List<User> users = new List<User>
        {
            new User
            {
                UserID = 1,
                Name = "Raghad",
                Password = "ra123",
                EmailAddress = "raghad@example.com",
                PaymentID = 5,
                Phone = "0555764524",
                UserRole = Entity.User.Role.Admin,
            },
            new User
            {
                UserID = 2,
                Name = "Reema",
                Password = "97643",
                EmailAddress = "reema@example.com",
                PaymentID = 10,
                Phone = "0534201235",
                UserRole = Entity.User.Role.Guest,
            },
            new User
            {
                UserID = 3,
                Name = "Ali",
                Password = "746953",
                EmailAddress = "ali@example.com",
                PaymentID = 7,
                Phone = "0557398543",
                UserRole = Entity.User.Role.User,
            },
        };

        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            User? foundUser = users.FirstOrDefault(u => u.UserID == id);
            if (foundUser == null)
            {
                return NotFound();
            }
            return Ok(foundUser);
        }

        [HttpPost]
        public ActionResult AddUser(User newUser)
        {
            users.Add(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserID }, newUser);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            User? foundUser = users.FirstOrDefault(u => u.UserID == id);
            if (foundUser == null)
            {
                return NotFound();
            }
            users.Remove(foundUser);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User updatedUser)
        {
            User? foundUser = users.FirstOrDefault(u => u.UserID == id);
            if (foundUser == null)
            {
                return NotFound();
            }
            foundUser.Name = updatedUser.Name;
            return Ok(foundUser);
        }

        [HttpPost("signup")]
        public ActionResult SignUpUser([FromBody] User user)
        {
            PasswordUtils.HashPassword(user.Password, out string hashedPass, out byte[] salt);
            user.Password = hashedPass;
            user.Salt = salt;
            users.Add(user);
            return Created($"/api/v1/user/{user.UserID}", user);
        }

        [HttpPost("login")]
        public ActionResult LogIn(User user)
        {
            User? foundUser = users.FirstOrDefault(p => p.EmailAddress == user.EmailAddress);
            if (foundUser == null)
            {
                return NotFound();
            }

            bool isMatched = PasswordUtils.VerifyPassword(
                user.Password,
                foundUser.Password,
                foundUser.Salt
            );
            if (!isMatched)
            {
                return Unauthorized();
            }
            return Ok(foundUser);
        }
    }
}
