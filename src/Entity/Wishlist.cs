using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class Wishlist
    {
        public Guid WishlistID { get; set; }
        public Guid ProductID { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid UserID { get; set; }
    }
}
