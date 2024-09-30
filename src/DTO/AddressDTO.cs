using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO
{
    public class AddressDTO
    {
        // Class to : create Address

        public class AddressCreateDto()
        {
            public string? Country { get; set; }
            public string? Street { get; set; }
            public Guid UserId { get; set; }
        }

        // Class to : get/read data

        public class AddressReadDto()
        {
            public Guid AddressId { get; set; }
            public string? Country { get; set; }
            public string? Street { get; set; }
            public Guid UserId { get; set; }
        }

        // Class to : Update Address

        public class AddressUpdateDto()
        {
            public string? Country { get; set; }
            public string? Street { get; set; }
        }
    }
}
