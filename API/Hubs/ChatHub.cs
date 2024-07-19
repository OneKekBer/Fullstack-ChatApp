using API.Database;
using API.Domains.Chat.Models;
using API.Domains.User.Models;
using API.Exceptions;
using API.Exceptions.Auth;
using API.Models;
using API.Server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace API.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string userName, string message);

        Task ReceiveChats(List<ChatGroup> ChatGroup);

        Task UpdateChat(ChatGroup chatGroup);

        Task OnlineUsers(List<OnlineUserData> users);

        Task CreateNewChat(ChatGroup chat);


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

            var allUserChats = _chatDatabase.GetAllUserChats(user.Login);

            await Clients.Client(user.ConnectionId).ReceiveChats(allUserChats);
            await Clients.All.OnlineUsers(formatedUserData);    
        }

        public async Task CreateGroup(string userTwoConnectionId) //text somebody
        {
            var userOne = _usersDatabase.FindUserByConnectionId(Context.ConnectionId);
            var userTwo = _usersDatabase.FindUserByConnectionId(userTwoConnectionId);

            string groupName = $"{userOne.Login}-{userTwo.Login}";

            await Groups.AddToGroupAsync(userTwoConnectionId, groupName);
            await Groups.AddToGroupAsync(userOne.ConnectionId, groupName);

            var isChatExists = _chatDatabase.IsChatExists(userOne.Login, userTwo.Login);

            if (isChatExists)
                throw new Exception("chat already exists");

            try
            {
                var newChat = new ChatGroup( // there are was a bug because 
                    new List<UserSafeData>() 
                    {
                        new UserSafeData(userOne.Login, userOne.ConnectionId),
                        new UserSafeData(userTwo.Login, userTwo.ConnectionId) 
                    },
                    new List<Message>() { },
                    groupName
                    );

                _logger.LogInformation("chatId: " + newChat.Id);
                _logger.LogInformation("user one Id " + newChat.Users[0].Id);
                _logger.LogInformation("user two Id " + newChat.Users[1].Id);


                _chatDatabase.ChatGroups.Add(newChat);
                _chatDatabase.SaveChanges();

                await Clients.Group(groupName).CreateNewChat(newChat);

            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task SendMessage(string authorLogin, string text, string groupName) // question: should I in this situation use records for data?
        {
            // question: should i every time use findByName, findById methods in backend;
            // or i should from frontend send more buisness data and contain it for exmpl: connectionId, Id and ect 

            var user = _usersDatabase.FindUserByLogin(authorLogin); // like here i want to get connectionId

            var chat = _chatDatabase.FindChatGroupByName(groupName);

            var newMessage = (new Message(new UserSafeData(user.Login, user.ConnectionId), text));

            chat.Messages.Add(newMessage);

            _chatDatabase.Update(chat);
            _chatDatabase.SaveChanges();

            await Clients.Group(groupName).UpdateChat(chat);
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