using API.Models;
using API.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace API.Database
{
    public class ChatDB : DbContext
    {
        public ChatDB(DbContextOptions<ChatDB> options)
            : base(options)
        { 
        }

        public DbSet<ChatGroup> ChatGroups { get; set; }

        public void UpdateChat(ChatGroup chatGroup)
        {
            var oldChat = ChatGroups.FirstOrDefault(x => x.Name == chatGroup.Name);

            if (oldChat != null)
            {
                oldChat.Messages = chatGroup.Messages;
                var contains = ChatGroups.Contains(oldChat);
                Console.WriteLine($"contains?: {contains}");
                Update(oldChat);
                SaveChanges();
            }
        }

        public List<ChatGroup> GetAllUserChats(string login)
        {
            return ChatGroups
                .Where(x => x.Users
                    .Any(x => x.Login == login))
                .ToList();
        }

        public bool IsChatExists(string userOneLogin, string userTwoLogin)
        {
            var chat = ChatGroups.FirstOrDefault(x =>
                x.Users.Any(u => u.Login == userOneLogin) 
                &&
                x.Users.Any(u => u.Login == userTwoLogin)
            );

            return chat is not null;
        }

        public ChatGroup FindChatGroupByName(string name)
        {
            var searchingGroup = ChatGroups.FirstOrDefault(x => x.Name == name) ?? throw new NullReferenceException("group is undefind");

            return searchingGroup;
        }

        public ChatGroup FindChatGroupById(Guid id)
        {
            var searchingGroup = ChatGroups.FirstOrDefault(x => x.Id == id) ?? throw new NullReferenceException("group is undefind");

            //searchingGroup.Messages = searchingGroup.Messages.Take(20).ToList();

            return searchingGroup;
        }
    }
}
