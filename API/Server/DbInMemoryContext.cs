using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace API.Server
{
    
    public class ChatDB : DbContext
    {
        public ChatDB(DbContextOptions<ChatDB> options)
            : base(options)
        { }

        public DbSet<ChatGroup> ChatGroups { get; set; }
    }
}
