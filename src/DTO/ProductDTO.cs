namespace src.DTO
{
    public class ProductDTO
    {

        public class ProductCreateDto
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public Guid CategoryId { get; set; }
        }
        public class ProductReadDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string? Description { get; set; }
            public CategoryDTO.CategoryReadDto? Category{ get; set; }
        }

        public class ProductUpdateDto
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string? Description { get; set; }
            public Guid? CategroyId { get; set; }
            public OrderDTO.Get? Order { get; set; }
        }
    }


}


