using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity
{
    public class Adress
    {
        public int? AddressId { get; set; }
        public string? Country { get; set; }
        public string? Street { get; set; }
        public int? UserId { get; set; }
    }
}
