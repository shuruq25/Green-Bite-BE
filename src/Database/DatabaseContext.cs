using Microsoft.EntityFrameworkCore;
using src.Entity;

namespace src.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
