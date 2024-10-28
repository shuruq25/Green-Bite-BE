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
        public DbSet<MealPlan> MealPlan { get; set; }
        public DbSet<MealPlanMeal> MealPlanMeal { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<DietaryGoal> DietaryGoal { get; set; }


        public DbSet<OrderDetails> OrderDetails { get; set; }



        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
        .Property(o => o.Status)
        .HasConversion<string>();
            modelBuilder.Entity<MealPlan>()
                 .Property(m => m.Type)
                 .HasConversion<string>();
            modelBuilder.Entity<Subscription>()
         .Property(s => s.Status)
         .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                   .Property(o => o.Method)
                   .HasConversion<string>();
            modelBuilder.Entity<Payment>()
           .Property(o => o.Status)
           .HasConversion<string>();

            modelBuilder.Entity<User>()
           .Property(u => u.UserRole)
           .HasConversion<string>();

            modelBuilder
                .Entity<User>()
                .HasMany(user => user.Orders)
                .WithOne(order => order.User)
                .HasForeignKey(order => order.UserID)
                .HasPrincipalKey(user => user.UserID);

            modelBuilder
                .Entity<Order>()
                .HasOne(order => order.Payment)
                .WithOne(payment => payment.Order)
                .HasForeignKey<Order>(order => order.PaymentID);

            modelBuilder.Entity<User>().HasIndex(u => u.EmailAddress).IsUnique();
            modelBuilder.Entity<Cart>().HasIndex(u => u.UserId).IsUnique();

            modelBuilder.Entity<Review>()
            .HasOne(r => r.Order) // Each Review has one Order
            .WithMany(o => o.Reviews) // Each Order can have many Reviews
            .HasForeignKey(r => r.OrderId); // The foreign key in Review is OrderId

        }
    }
}