namespace src.Entity
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string? Country { get; set; }
        public string? Street { get; set; }
        public User? UserId { get; set; }
    }
}
