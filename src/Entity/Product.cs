namespace src.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid? CategroyId { get; set; }
        public Order? Order { get; set; }
    }
}
