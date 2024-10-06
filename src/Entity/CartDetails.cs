namespace src.Entity
{
    public class CartDetails
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CartId { get; set; }

        /*[Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]*/
        /*public int Quantity { get; set; }*/
    }
}
