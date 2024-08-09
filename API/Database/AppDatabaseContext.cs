using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options)
            : base(options)
        { 
        }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
