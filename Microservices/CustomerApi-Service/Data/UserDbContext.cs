using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UsersApi.Data
{
    public class UserDbContext : IdentityDbContext<UserEntity>
    {

        public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
        {



        }


        public DbSet<UserEntity> UserEntity { get; set; }



    }
}
