using System.ComponentModel.DataAnnotations;

namespace src.Entity
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid? OrderId { get; set; }

        [MaxLength(100), MinLength(5)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Comment must contain only letters")]
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public Guid UserID { get; set; }
        public Guid ProductId { get; set; }
         public Product Product { get; set; } 


    }
}
