using System.ComponentModel.DataAnnotations;

namespace src.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [MaxLength(250), MinLength(5)]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Description must contain only letters, numbers ."
        )]
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
