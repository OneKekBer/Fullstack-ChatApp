using Microsoft.AspNetCore.SignalR;
using System.Data;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChat(string chatName, string name)
        {
            if (chatName == string.Empty || name == string.Empty)
                return;
            await Groups.AddToGroupAsync(Context.ConnectionId, chatName);

            await Clients.Group(chatName).SendAsync("ReciveMessage", $"{name} has joined!");
        }
        
        public async Task SendMessage(string chatName, string text)
        {
            Console.WriteLine(text);
            await Clients.Group(chatName).SendAsync("ReciveMessage", text);
        }
    }
}