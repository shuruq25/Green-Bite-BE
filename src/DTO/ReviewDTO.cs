using System.ComponentModel.DataAnnotations;

namespace src.DTO
{
    public class ReviewDTO
    {
        public class ReviewCreateDto
        {
            public string? Comment { get; set; }

            public int Rating { get; set; }

            public Guid? OrderId { get; set; }
             public Guid ProductId { get; set; }

        }

        public class ReviewReadDto
        {
            public Guid ReviewId { get; set; }
            public Guid? OrderId { get; set; }
            public string? Comment { get; set; }
            public int Rating { get; set; }
            public DateTime ReviewDate { get; set; }
            public Guid UserID { get; set; }
            public DateTime CreatedAt { get; set; }
             public Guid ProductId { get; set; }

        }

        public class ReviewUpdateDto
        {
            public string? Comment { get; set; }
            public int Rating { get; set; }
             public Guid ProductId { get; set; }

        }
    }
}
