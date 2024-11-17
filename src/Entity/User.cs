using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using src.Utils;

namespace src.Entity
{
    public class User
    {
        public Guid UserID { get; set; }
        public string? Name { get; set; }
        [Required]
        [PasswordComplexity]
        public string Password { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please provide email with right format")]
        public string EmailAddress { get; set; }
        public string? Phone { get; set; }
        public Role UserRole { get; set; } = Role.Customer;
        public byte[]? Salt { get; set; }
        public string? Gender { get; set; }
        public decimal? Weight { get; set; }
        public int? Age { get; set; }


        public ICollection<Order> Orders { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Role
        {
            Admin,
            Customer,
        }
    }
}
