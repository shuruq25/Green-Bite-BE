using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using src.DTO;
using src.Utils;

namespace src.Entity
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }

        [PasswordComplexity]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Please provide email with right format")]
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public Role UserRole { get; set; } = Role.Customer;
        public byte[]? Salt { get; set; }
        public ICollection<Order> Orders { get; set; }

        public static implicit operator User(UserDTO.UserCreateDto v)
        {
            throw new NotImplementedException();
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Role
        {
            Admin,
            Customer,
        }
    }
}
