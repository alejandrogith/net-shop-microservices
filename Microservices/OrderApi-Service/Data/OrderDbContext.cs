using Microsoft.EntityFrameworkCore;

namespace OrdersApi.Data
{
    public class OrderDbContext : DbContext
    {

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {

        }


        public DbSet<OrderDetailEntity> OrderDetailEntity { get; set; }

        public DbSet<OrderMasterEntity> OrderMasterEntity { get; set; }
    }
}
