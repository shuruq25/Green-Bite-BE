namespace src.Entity
{
    public class Order
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public decimal OriginalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public OrderStatus Status { get; set; }
        public enum OrderStatus
        {
            Pending,
            Shipped,
            Delivered
        }
        public int PaymentID { get; set; }
    }
}
