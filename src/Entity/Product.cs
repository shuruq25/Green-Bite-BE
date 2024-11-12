using System.ComponentModel.DataAnnotations;

namespace src.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public required decimal Price { get; set; }
        public  string ImageUrl { get; set; }

        [MaxLength(250), MinLength(5)]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]+$",
            ErrorMessage = "Description must contain only letters, numbers and spaces."
        )]

        public required string Description { get; set; }

        public string NutritionalInfo { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public double? AveReviews { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public Guid? DietaryGoalId { get; set; }
        public DietaryGoal? DietaryGoal{ get; set; }
        public string Allergies{ get; set; }
    }
}
