using System.Text.Json.Serialization;

namespace src.Entity
{
    public class User
    {
        public Guid UserID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public Role UserRole { get; set; } = Role.Customer;
        public byte[]? Salt { get; set; }
        public List<Order> Orders { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Role
        {
            Admin,
            Customer,
        }
    }
}
