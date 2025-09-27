using ChatApp.Data.Entities;
using ChatApp.Data.Repository.Interfaces;
using ChatApp.Presentation.Domains.Chat;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Presentation.Hubs
{
    public interface IChatClient
    {
        Task SendChatMessages(string chatName,IEnumerable<Message> messages);
        Task UpdateChat(string chatName, Message message);
        Task GetAllChats(List<Chat> chats);
        Task SendError(string error);
    }

    public partial class ChatHub : Hub<IChatClient>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IUserRepository userRepository, IChatRepository chatRepository, ILogger<ChatHub> logger, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task Connect(string login)
        {
            var user = await _userRepository.GetByLogin(login);
            await _chatRepository.AddConnectionIdToAllUserChats(user, Context.ConnectionId);
            var chats = _chatRepository.GetUserChats(user.Id).Result;

            await Clients.Client(Context.ConnectionId).GetAllChats(chats);
        }

        public async Task JoinChat(JoinChatDto dto)
        {
            var chat = await _chatRepository.GetByName(dto.Name);

            if (chat is null)
            {
                await Clients.Client(Context.ConnectionId).SendError($"Cannot find chat {dto.Name}");
                _logger.LogWarning("NotFoundInDatabaseException ");
                return;
            }

            var user = await _userRepository.GetByLogin(dto.Login);

            await _chatRepository.AddUserIdToChat(chat, user.Id); 
            await _chatRepository.AddConnectionIdToChat(chat, Context.ConnectionId);

            var messages = await _messageRepository.GetChatMessages(chat.Id);

            await Clients.Client(Context.ConnectionId).SendChatMessages(chat.Name, messages);
        }

        public async Task GetChatMessages(GetChatMessagesDto dto)
        {
            var chat = await _chatRepository.GetByName(dto.Name);

            var messages = await _messageRepository.GetChatMessages(chat.Id);

            await Clients.Client(Context.ConnectionId).SendChatMessages(dto.Name, messages);
        }
        
        public async Task SendMessage(SendMessageDto dto)
        {
            if (dto.Message.Length == 0)
                return;

            var user = _userRepository.GetByLogin(dto.AuthorLogin).Result;
            var chat = _chatRepository.GetByName(dto.ChatName).Result;

            var newMessage = new Message(user.Id, chat.Id, dto.Message, user.Login);
            await _messageRepository.Add(newMessage);

            await Clients.Clients(chat.ConnectionIds).UpdateChat(dto.ChatName, newMessage);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if(Context.ConnectionId != null) 
            {
                await _chatRepository.RemoveConnectionIdInAllChats(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}