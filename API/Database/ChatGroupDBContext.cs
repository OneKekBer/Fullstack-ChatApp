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

        public bool IsChatExists(string userOneLogin, string userTwoLogin)
        {
            var chat = ChatGroups.FirstOrDefault(x =>
                x.Users.Any(u => u.Login == userOneLogin) 
                &&
                x.Users.Any(u => u.Login == userTwoLogin)
            );

            return chat != null;
        }

        public ChatGroup FindChatGroupById(Guid id)
        {
            var searchingGroup = ChatGroups.FirstOrDefault(x => x.Id == id) ?? throw new NullReferenceException("group is undefind");

            searchingGroup.Messages = searchingGroup.Messages.Take(20).ToList();

            return searchingGroup;
        }
    }
}
