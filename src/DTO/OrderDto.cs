using src.Entity;
using static src.DTO.ReviewDTO;
namespace src.DTO
{
    public class OrderDTO
    {
        public class Update
        {
            public decimal? OriginalPrice { get; set; }
            public DateTime? EstimatedArrival { get; set; }
            public OrderStatuses? Status { get; set; }
        }

        public class Create
        {
            public decimal OriginalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime EstimatedArrival { get; set; }
            public OrderStatuses Status { get; set; }
        }

        public class Get
        {
            public Guid ID { get; set; }
            public Guid UserID { get; set; }
            public decimal OriginalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime EstimatedArrival { get; set; }
            public OrderStatuses Status { get; set; }
            public Guid PaymentID { get; set; }
            public List<ReviewReadDto> Reviews { get; set; }
        }
    }
}
