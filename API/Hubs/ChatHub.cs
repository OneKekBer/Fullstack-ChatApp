using API.Domains.User.Models;
using API.Exceptions;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.SignalR;
using System;
using static API.Hubs.ChatHub;

namespace API.Hubs
{
    public interface IChatClient
    {
        Task SendChatMessages(string chatName,IEnumerable<Message> messages);
        Task UpdateChat(string chatName, Message message);
        Task GetAllChats(List<Chat> chats);
        Task SendError(string error);
    }

    public class ChatHub : Hub<IChatClient>
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
        public record JoinChatDTO(string login, string chatName);
        public async Task JoinChat(JoinChatDTO joinChatDTO)
        {
            _logger.LogInformation(joinChatDTO.chatName);
            _logger.LogInformation(joinChatDTO.login);

            var chat = await _chatRepository.GetByName(joinChatDTO.chatName);

            if (chat is null)
            {
                await Clients.Client(Context.ConnectionId).SendError($"Cannot find chat {joinChatDTO.chatName}");
                throw new NotFoundInDatabaseException();
            }

            var user = await _userRepository.GetByLogin(joinChatDTO.login);

            await _chatRepository.AddUserIdToChat(chat, user.Id);
            await _chatRepository.AddConnectionIdToChat(chat, Context.ConnectionId);

            var messages = await _messageRepository.GetChatMessages(chat.Id);

            await Clients.Client(Context.ConnectionId).SendChatMessages(chat.Name, messages);
        }
        public record GetChatMessagesDTO(string chatName);
        public async Task GetChatMessages(GetChatMessagesDTO getChatMessagesDTO)
        {
            _logger.LogInformation("get chat messages is working!!");
            var chat = await _chatRepository.GetByName(getChatMessagesDTO.chatName);

            var messages = await _messageRepository.GetChatMessages(chat.Id);

            await Clients.Client(Context.ConnectionId).SendChatMessages(getChatMessagesDTO.chatName, messages);
        }

        public async Task SendMessage(string authorLogin, string text, string chatName)
        {
            if (text.Length == 0)
                return;

            var user = _userRepository.GetByLogin(authorLogin).Result;
            var chat = _chatRepository.GetByName(chatName).Result;

            var newMessage = new Message(user.Id, chat.Id, text, user.Login);
            await _messageRepository.Add(newMessage);

            await Clients.Clients(chat.ConnectionIds).UpdateChat(chatName, newMessage);
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