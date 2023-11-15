using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Data
{
    public class ProductDbContext : DbContext
    {

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {

        }


        public DbSet<ProductEntity> ProductEntity { get; set; }


    }
}
