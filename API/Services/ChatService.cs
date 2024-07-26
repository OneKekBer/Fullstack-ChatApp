
using API.Domains.Chat.Models;
using API.Models;
using API.Repository;

namespace API.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatRepository _chatRepository;

        public ChatService(ChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task CreateChat(CreateChatDTO createChatDTO)
        {
            var chat = new Chat(createChatDTO.Name);

            await _chatRepository.Add(chat);
        }
    }
}
