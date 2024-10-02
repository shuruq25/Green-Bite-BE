using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using static src.DTO.CartDetailsDTO;

namespace src.DTO
{
    public class CartDTO
    {
        public class CartCreateDto
        {
            public List<CartDetailsCreateDto> CartDetails { get; set; } = new List<CartDetailsCreateDto>();
            public int CartTotal { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class CartReadDto
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public List<CartDetails> CartDetails { get; set; } =new List<CartDetails>();
            public int CartTotal { get; set; }
            public decimal TotalPrice { get; set; }

        }

        public class CartUpdateDto
        {

        public List<CartDetails> CartDetails { get; set; }= new List<CartDetails>();
         public int CartTotal { get; set; }
         public decimal TotalPrice { get; set; }
        }


    }
}