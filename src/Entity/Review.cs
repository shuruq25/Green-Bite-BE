using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int OrderId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
