using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Server
{
    public class UsersDatabase : DbContext
    {
        public UsersDatabase(DbContextOptions<UsersDatabase> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
