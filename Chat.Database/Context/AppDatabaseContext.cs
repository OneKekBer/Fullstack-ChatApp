using Microsoft.EntityFrameworkCore;

namespace Database.Context
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options)
            : base(options)
        { 
        }

        //ensure create

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
