namespace src.Entity
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<CartDetails> CartDetails { get; set; }
        public int CartTotal { get; set; }
        public decimal TotalPrice { get; set; }



    }
}