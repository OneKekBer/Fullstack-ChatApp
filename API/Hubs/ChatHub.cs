using API.Exceptions;
using API.Server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Xml.Linq;
using static API.Hubs.ChatHub;





namespace API.Hubs
{

    public interface IChatClient
    {
        public Task ReceiveMessage(string userName, string message);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMemoryCache _cache;
        private readonly ChatDB _db;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IMemoryCache cache, ChatDB db, ILogger<ChatHub> logger)
        {
            _cache = cache;
            _db = db;
            _logger = logger;
        }

        public record UserData(string GroupName, string Name);

        public async Task JoinChat(UserData userData)
        {
            var (GroupName, Name) = userData;

            _logger.LogInformation("GroupName: " +GroupName);

            if (GroupName == string.Empty || Name == string.Empty)
                return;

            _cache.Set(Context.ConnectionId, userData);
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);

            await Clients.Group(GroupName).ReceiveMessage("Admin", $"{Name} has joined!");
        }
        
        public async Task SendMessage(string chatName, string text)
        {
            Console.WriteLine(text);

            _cache.TryGetValue(Context.ConnectionId, out UserData? userData);

            if (userData == null)
                throw new CacheIsNull();

            await Clients.Group(chatName).ReceiveMessage(userData.Name, text);

        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _cache.TryGetValue(Context.ConnectionId, out UserData? userData);

            if (userData is not null)
            {
                _cache.Remove(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userData.GroupName);

                await Clients.Group(userData.GroupName).ReceiveMessage("Admin", $"{userData.Name} was disconnected!");

            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}