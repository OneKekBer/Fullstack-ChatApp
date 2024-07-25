//using API.Database;
//using API.Domains.Chat.Models;
//using API.Domains.User.Models;
//using API.Exceptions;
//using API.Exceptions.Auth;
//using API.Models;
//using API.Repository;
//using API.Server;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.Caching.Memory;

//namespace API.Hubs
//{
//    public interface IChatClient
//    {
//        Task ReceiveMessage(string userName, string message);

//        Task ReceiveChats(List<Chat> ChatGroup);

//        Task UpdateChat(Chat chatGroup);

//        Task OnlineUsers(List<OnlineUserData> users);

//        Task CreateNewChat(Chat chat);
//    }

//    public class ChatHub : Hub<IChatClient>
//    {
//        private readonly ChatRepository _chatRepository;
//        private readonly UserRepository _userRepository;
//        private readonly ILogger<ChatHub> _logger;

//        public ChatHub(UserRepository userRepository, ChatRepository chatRepository, ILogger<ChatHub> logger)
//        {
//            _userRepository = userRepository;
//            _chatRepository = chatRepository;
//            _logger = logger;
//        }

//        public async Task Connect(string login)
//        {
//            _logger.LogInformation($"{login} connected!");
//            var user = await _userRepository.GetByLogin(login);
//        }

//        public async Task CreateGroup(string userTwoConnectionId) //text somebody
//        {
//            var isChatExists = _chatDatabase.IsChatExists(userOne.Login, userTwo.Login);

//            if (isChatExists)
//                throw new Exception("chat already exists");

//            try
//            {
//                var newChat = new ChatGroup( // there are was a bug because 
//                    new List<UserSafeData>() 
//                    {
//                        new UserSafeData(userOne.Login, userOne.ConnectionId),
//                        new UserSafeData(userTwo.Login, userTwo.ConnectionId) 
//                    },
//                    new List<Message>() { },
//                    groupName
//                );

//                _chatDatabase.Add(newChat);
//                _chatDatabase.SaveChanges();

//                await Clients.Group(groupName).CreateNewChat(newChat);

//            }
//            catch(Exception ex) 
//            {
//                _logger.LogError(ex.Message);
//            }
//        }

//        public async Task SendMessage(string authorLogin, string text, string groupName) // question: should I in this situation use records for data?
//        {
//            // question: should i every time use findByName, findById methods in backend;
//            // or i should from frontend send more buisness data and contain it for exmpl: connectionId, Id and ect ..

//            var user = _usersDatabase.FindUserByLogin(authorLogin); // like here i want to get connectionId

//            var chat = _chatDatabase.FindChatGroupByName(groupName);

//            var newMessage = new Message(new UserSafeData(user.Login, user.ConnectionId), text);

//            chat.Messages.Add(newMessage);

//            _chatDatabase.UpdateChat(chat);
//            _chatDatabase.SaveChanges();

//            await Clients.Group(groupName).UpdateChat(chat);
//        }

//        public override async Task OnDisconnectedAsync(Exception? exception)
//        {
//            var user = _usersDatabase.FindUserByConnectionId(Context.ConnectionId); 
            
//            if (user is not null)
//            {
//                user.ConnectionId = "";
//                user.Status = UserStatus.Offline;

//                _usersDatabase.Update(user);
//                _usersDatabase.SaveChanges();

//                var onlineUsers = _usersDatabase.GetOnlineUsers();
//                var formatedUserData = _usersDatabase.FormatUserDataToOnlineUserData(onlineUsers);

//                await Clients.All.OnlineUsers(formatedUserData);
//            }   

//            await base.OnDisconnectedAsync(exception);
//        }
//    }
//}