using aggregate_root_ddd_data_app.Models;
using Microsoft.EntityFrameworkCore;

namespace aggregate_root_ddd_data_app
{
    public class OrdersDbContext : DbContext
    {
        // Add DbSet properties for each entity you want to access in the database
        public DbSet<OrderModel> Orders { get; set; }

        // Constructor to receive DbContextOptions from dependency injection
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }
    }
}
