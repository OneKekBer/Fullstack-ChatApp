using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Server
{
    public class MessagesDatabase : DbContext
    {
        public MessagesDatabase(DbContextOptions<MessagesDatabase> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
