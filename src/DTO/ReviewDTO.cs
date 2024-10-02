using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;

namespace src.DTO
{
    public class ReviewDTO
    {
        public class ReviewCreateDto
        {
            public string? Comment { get; set; }

            [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
            public int Rating { get; set; }

            public Guid OrderId { get; set; }
            //???????
            //public Guid UserID {get; set;}
        }

        public class ReviewReadDto
        {
            public Guid ReviewId { get; set; }
            public Guid OrderId { get; set; }
            public string? Comment { get; set; }
            public int Rating { get; set; }
            public DateTime ReviewDate { get; set; }


            public Guid UserID { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class ReviewUpdateDto
        {
            public string? Comment { get; set; }
            public int Rating { get; set; }
        }
    }
}
