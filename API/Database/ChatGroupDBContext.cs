using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace API.Database
{
    
    public class ChatDB : DbContext
    {
        public ChatDB(DbContextOptions<ChatDB> options)
            : base(options)
        { }

        public DbSet<ChatGroup> ChatGroups { get; set; }

        public void FindChatGroupById()
        {

        }
    }
}
