namespace src.DTO
{
    public class CartDetailsDTO
    {
        public class CartDetailsCreateDto
        {
            public Guid ProductId { get; set; }

            /*public int Quantity { get; set; }*/
        }

        public class CartDetailsReadDto
        {
            public Guid Id { get; set; }
            /*public int Quantity { get; set; }*/
        }

        public class CartDetailsUpdateDto
        {
            public Guid ProductId { get; set; }
            /*public int Quantity { get; set; }*/
        }
    }
}
