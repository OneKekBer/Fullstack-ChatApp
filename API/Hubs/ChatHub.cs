using API.Domains.User.Models;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public interface IChatClient
    {
        Task GetChatMessages(string chatName,IEnumerable<Message> messages);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly ChatRepository _chatRepository;
        private readonly UserRepository _userRepository;
        private readonly MessageRepository _messageRepository;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(UserRepository userRepository, ChatRepository chatRepository, ILogger<ChatHub> logger, MessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task Connect(string login)
        {
            
        }

        public async Task JoinChat(Guid chatId)
        {
            var chat = await _chatRepository.GetById(chatId);
            await _chatRepository.AddConnectionIdToChat(chat, Context.ConnectionId);
            var messages = await _messageRepository.GetChatMessages(chat.Id);

            await Clients.Client(Context.ConnectionId).GetChatMessages(chat.Name, messages); // Idea: in the future send 20 messages
                                                                                  // and when user want to get all messages 
                                                                                  //send them
        }

        public async Task SendMessage(string authorLogin, string text, string groupName) // question: should I in this situation use records for data?
        {
            
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