namespace src.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartTotal { get; set; }
        public decimal TotalPrice { get; set; }

    }
}