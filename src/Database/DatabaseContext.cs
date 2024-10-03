using Microsoft.EntityFrameworkCore;
using src.Entity;
using static src.Entity.Payment;
using static src.Entity.User;

namespace src.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }



        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasPostgresEnum<OrderStatuses>();
            modelBuilder.HasPostgresEnum<PaymentMethod>();
            modelBuilder.HasPostgresEnum<PaymentStatus>();
            modelBuilder.HasPostgresEnum<Role>();
        }
    }
}
