namespace src.Entity
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<CartDetails> CartDetails { get; set; }
        public int CartTotal => CartDetails.Count;
        public decimal TotalPrice => CartDetails.Sum(cd => cd.Product.Price /* * cd.Quantity*/);



    }
}
