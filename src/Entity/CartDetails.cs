namespace src.Entity
{
    public class CartDetails
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CartId { get; set; }
       // public Cart Cart{ get; set; }   
        public int Quantity { get; set; }

    }
}