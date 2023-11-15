using Microsoft.EntityFrameworkCore;

namespace InventoryApi_Service.Data
{
    public class InventoryDbContext : DbContext
    {

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {

        }


        public DbSet<InventoryEntity> InventoryEntity { get; set; }


    }
}
