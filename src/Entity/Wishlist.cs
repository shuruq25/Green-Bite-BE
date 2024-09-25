using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class Wishlist
    {
        public int Wishlist_ID { get; set; }
        public int Product_ID { get; set; }
        public DateTime Date_Added { get; set; }
    }
}
