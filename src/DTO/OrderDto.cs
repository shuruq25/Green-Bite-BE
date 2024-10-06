using src.Entity;
using static src.DTO.OrderDetailDTO;

using static src.DTO.ReviewDTO;

namespace src.DTO
{
    public class OrderDTO
    {
        public class Update
        {
            public DateTime? EstimatedArrival { get; set; }
            public OrderStatuses? Status { get; set; }
        }

        public class Create
        {
            public DateTime CreatedAt { get; set; }
            public DateTime EstimatedArrival { get; set; }
            public OrderStatuses Status { get; set; }
            public Guid UserID { get; set; }
            public List<ProductDTO.ProductReadDto> Products { get; set; }
        }

        public List<OrderDetailCreateDto> OrderDetails { get; set; }

        public class Get
        {
            public Guid ID { get; set; }
            public UserDTO.UserReadDto User { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime EstimatedArrival { get; set; }
            public OrderStatuses Status { get; set; }
            public PaymentDTO.PaymentReadDto? Payment { get; set; }
            public List<ReviewReadDto> Reviews { get; set; }
            public List<ProductDTO.ProductReadDto> Products { get; set; }
            public decimal OriginalPrice => Products.Sum(p => p.Price);
        }
    }


}
