namespace src.Entity
{

    public enum OrderStatuses
    {
        Pending,
        Shipped,
        Delivered
    }
    public class Order
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public decimal OriginalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public OrderStatuses Status { get; set; }
        public Guid PaymentID { get; set; }
    }
}
