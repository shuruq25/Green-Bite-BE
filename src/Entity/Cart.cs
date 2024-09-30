namespace src.Entity
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CartTotal { get; set; }
        public decimal TotalPrice { get; set; }

    }
}