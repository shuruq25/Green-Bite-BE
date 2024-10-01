using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO
{
    public class WishlistDTO
    {
        public class WishlistCreateDto
        {
            public Guid ProductID { get; set; }
            public Guid UserID { get; set; }
            public DateTime DateAdded { get; set; }
        }

        public class WishlistReadDto
        {
            public Guid WishlistID { get; set; }
            public Guid ProductID { get; set; }
            public DateTime DateAdded { get; set; }
            public Guid UserID { get; set; }
        }

        public class WishlistUpdateDto
        {
            public Guid ProductID { get; set; }
            public Guid UserID { get; set; }
        }

    }
}