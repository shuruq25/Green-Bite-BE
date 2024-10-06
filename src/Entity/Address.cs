namespace src.Entity
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public Guid UserId { get; set; } // (FK) from User table
        public User User { get; set; } // To provide us the information about User
    }
}
