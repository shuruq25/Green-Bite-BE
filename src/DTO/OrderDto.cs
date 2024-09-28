using src.Entity;
namespace src.DTO
{
    public class OrderDTO
    {
        public class Update
        {
            public decimal? OriginalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? EstimatedArrival { get; set; }
            public OrderStatuses? Status { get; set; }
            public int? PaymentID { get; set; }
        }

        public class Create
        {
            public int UserID { get; set; }
            public decimal OriginalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime EstimatedArrival { get; set; }
            public OrderStatuses Status { get; set; }
            public int PaymentID { get; set; }
        }

       public class Get
        {
            public int ID { get; set; }
            public int UserID { get; set; }
            public decimal OriginalPrice { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime EstimatedArrival { get; set; }
            public OrderStatuses Status { get; set; }
            public int PaymentID { get; set; }
        }
    }
}
