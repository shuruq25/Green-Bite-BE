using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static src.Entity.User;

namespace src.DTO
{
    public class UserDTO
    {
        public class UserCreateDto
        {
            public string? Name { get; set; }
            public string? Password { get; set; }
            public string? EmailAddress { get; set; }
            public string? Phone { get; set; }
        }

        public class UserSignInDto
        {
            public string EmailAddress { get; set; }
            public string Password { get; set; }
        }

        public class UserReadDto
        {
            public Guid UserID { get; set; }
            public string? Name { get; set; }
            public string? EmailAddress { get; set; }
            public string? Phone { get; set; }
            public Role UserRole { get; set; }
        }

        public class UserUpdateDto
        {
            public string? Name { get; set; }
            public string? EmailAddress { get; set; }
            public string? Phone { get; set; }
        }
    }
}
