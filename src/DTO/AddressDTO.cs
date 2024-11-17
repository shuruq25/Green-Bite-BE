using src.Entity;

namespace src.DTO
{
    public class AddressDTO
    {
        // Class to : create Address

        public class AddressCreateDto
        {
            public string Country { get; set; }
            public string Street { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string City { get; set; }
            public Guid UserId { get; set; } // (FK) from User table



        }

        // Class to : get/read data

        public class AddressReadDto
        {
            public Guid AddressId { get; set; }
            public string Country { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
         public Guid UserId { get; set; } // (FK) from User table


        }

        // Class to : Update Address

        public class AddressUpdateDto
        {
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Street { get; set; }
        }
    }
}
