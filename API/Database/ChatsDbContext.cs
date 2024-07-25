using API.Models;
using API.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace API.Database
{
    public class ChatDatabase : DbContext
    {
        public ChatDatabase(DbContextOptions<ChatDatabase> options)
            : base(options)
        { 
        }

        public DbSet<Chat> Chats { get; set; }
    }
}
