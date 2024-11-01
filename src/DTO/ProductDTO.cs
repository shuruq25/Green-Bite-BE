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
            public required string ImageUrl { get; set; }
            public required string Description { get; set; }
            public Guid CategoryId { get; set; }
            public string NutritionalInfo { get; set; }

        }
        public class ProductReadDto
        {
            public Guid Id { get; set; }
            public required string Name { get; set; }
            public required decimal Price { get; set; }
            public required string ImageUrl { get; set; }
            public required string Description { get; set; }
            public Category Category { get; set; }
            public string NutritionalInfo { get; set; }
            public ICollection<ReviewDTO.ReviewReadDto>? reviews { get; set; }
            public double? AveReviews { get; set; }



        }

        public class ProductUpdateDto
        {

            public required string Name { get; set; }
            public required decimal Price { get; set; }
            public required string ImageUrl { get; set; }
            public required string Description { get; set; }
            public Guid CategoryId { get; set; }
            public string NutritionalInfo { get; set; }

        }
    }


}


