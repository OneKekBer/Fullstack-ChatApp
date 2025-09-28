using ChatApp.Data.Entities;

namespace ChatApp.Presentation.Hubs;

public interface IChatClient
{
    Task SendChatMessages(string chatName,IEnumerable<Message> messages);
    Task UpdateChat(string chatName, Message message);
    Task GetAllChats(List<Chat> chats);
    Task SendError(string error);
}