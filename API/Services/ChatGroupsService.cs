using API.Database;
using API.Models;

namespace API.Services
{
    public class ChatGroupsService
    {
        private readonly ChatDB _chatDatabase;

        public ChatGroupsService(ChatDB chatDatabase = null)
        {
            _chatDatabase = chatDatabase;
        }

        public List<ChatGroup> GetUserGroups(User user)
        {
            //var userChatGroups = new List<ChatGroup>();
            //foreach (var groupId in user.AllGroupsId)
            //{
            //    var chat = _chatDatabase.FindChatGroupById(groupId);
            //    userChatGroups.Add(chat);
            //}
            //return userChatGroups;

            var userChatGroups = user.AllGroupsId
                .Select(groupId => _chatDatabase.FindChatGroupById(groupId))
                .Where(chatGroup => chatGroup != null)
                .ToList();

            return userChatGroups;
        }

    }
}
