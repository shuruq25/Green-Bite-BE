using src.Entity;
using static src.DTO.CartDetailsDTO;

namespace src.DTO
{
    public class CartDTO
    {
        public class CartCreateDto
        {
            public List<CartDetailsCreateDto> CartDetails { get; set; } =
                new List<CartDetailsCreateDto>();
        }

        public class CartReadDto
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public List<CartDetails> CartDetails { get; set; }
            public int CartTotal { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class CartUpdateDto
        {
            public List<CartDetailsUpdateDto> CartDetails { get; set; } =
                new List<CartDetailsUpdateDto>();
        }
    }
}
