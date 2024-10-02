using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using static src.DTO.CartDTO;
using static src.DTO.ProductDTO;

namespace src.DTO
{
    public class CartDetailsDTO
    {

        public class CartDetailsCreateDto
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public class CartDetailsReadDto
        {
            public Guid Id { get; set; }
            public Guid Product { get; set; }
            public Guid Cart { get; set; }
            public int Quantity { get; set; }
        }

        public class CartDetailsUpdateDto
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }

        }
    }
}