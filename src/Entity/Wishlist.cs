using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class Wishlist
    {
        public int WishlistID { get; set; }
        public int ProductID { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserID { get; set; }
    }
}
