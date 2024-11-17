using src.Entity;

namespace src.DTO
{
    public class ProductDTO
    {

        public class ProductListDto
        {
            public List<ProductReadDto> Products { get; set; }
            public int TotalCount { get; set; }
        }
        public class ProductCreateDto
        {
            public required string Name { get; set; }
            public required decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public required string Description { get; set; }
            public Guid? CategoryId { get; set; }
            public string NutritionalInfo { get; set; }
            public Guid? DietaryGoalId { get; set; }
            public string Allergies { get; set; }


        }
        public class ProductReadDto
        {
            public Guid Id { get; set; }
            public required string Name { get; set; }
            public required decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public required string Description { get; set; }
            public Category Category { get; set; }
            public string NutritionalInfo { get; set; }
            public ICollection<ReviewDTO.ReviewReadDto>? reviews { get; set; }
            public double? AveReviews { get; set; }
            public DietaryGoal DietaryGoal { get; set; }

            public string Allergies { get; set; }


            public DietaryGoal? DietaryGoalId { get; set; }


        }

        public class ProductUpdateDto
        {

            public string? Name { get; set; }
            public decimal? Price { get; set; }
            public string? ImageUrl { get; set; }
            public string? Description { get; set; }
            public Guid? CategoryId { get; set; }
            public string? NutritionalInfo { get; set; }
            public string? Allergies { get; set; }


        }
    }


}


