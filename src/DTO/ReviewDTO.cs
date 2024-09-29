using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO
{
    public class ReviewDTO
    {
        public class ReviewCreateDto
        {
            public Guid ReviewId { get; set; }
            public string? Comment { get; set; }
            public int Rating { get; set; }
        }

        public class ReviewReadDto
        {
            public Guid ReviewId { get; set; }
            public Guid OrderId { get; set; }
            public string? Comment { get; set; }
            public int Rating { get; set; }
            public DateTime ReviewDate { get; set; }
        }

        public class ReviewUpdateDto
        {
            public string? Comment { get; set; }
            public int Rating { get; set; }
        }
    }
}