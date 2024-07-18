using API.Database;
using API.Domains.Chat.Models;
using API.Domains.User.Models;
using API.Exceptions;
using API.Models;
using API.Server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;



namespace API.Hubs
{

    
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName, string message);

        Task OnlineUsers(List<OnlineUserData> users);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMemoryCache _cache;
        private readonly ChatDB _chatDatabase;
        private readonly UsersDB _usersDatabase;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IMemoryCache cache, ChatDB db,UsersDB usersDatabase, ILogger<ChatHub> logger)
        {
            _cache = cache;
            _chatDatabase = db;
            _usersDatabase = usersDatabase;
            _logger = logger;
        }

        //when user loged in
        //user gets new connectionId
        //and other users see that user is online
        public async Task Connect(string login)
        {
            _logger.LogInformation($"{login} connected!");
            var user = _usersDatabase.FindUserByLogin(login);

            user.ConnectionId = Context.ConnectionId;
            user.Status = UserStatus.Online;

            _usersDatabase.Update(user);
            _usersDatabase.SaveChanges();

            var onlineUsers = _usersDatabase.GetOnlineUsers();
            var formatedUserData = _usersDatabase.FormatUserDataToOnlineUserData(onlineUsers);

            await Clients.All.OnlineUsers(formatedUserData);    
        }

        //public async Task JoinChat(JoinChatDto userData)
        //{
        //    var (GroupName, Name) = userData;

        //    _logger.LogInformation("GroupName: " + GroupName);

        //    if (GroupName == string.Empty || Name == string.Empty)
        //        return;

        //    _cache.Set(Context.ConnectionId, userData);
        //    await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);

        //    await Clients.Group(GroupName).ReceiveMessage("Admin", $"{Name} has joined!");
        //}

        public async Task SendMessage(string text)
        {
            Console.WriteLine(text);

            _cache.TryGetValue(Context.ConnectionId, out JoinChatDto? userData);

            if (userData == null)
                throw new CacheIsNull();

            await Clients.Group(userData.GroupName).ReceiveMessage(userData.Name, text);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = _usersDatabase.FindUserByConnectionId(Context.ConnectionId); 
            
            if (user is not null)
            {
                user.ConnectionId = "";
                user.Status = UserStatus.Offline;

                _usersDatabase.Update(user);
                _usersDatabase.SaveChanges();

                var onlineUsers = _usersDatabase.GetOnlineUsers();
                var formatedUserData = _usersDatabase.FormatUserDataToOnlineUserData(onlineUsers);

                await Clients.All.OnlineUsers(formatedUserData);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}