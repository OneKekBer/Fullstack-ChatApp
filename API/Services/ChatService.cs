
using API.Domains.Chat.Models;
using API.Models;
using API.Repository;

namespace API.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task Create(CreateChatDTO createChatDTO)
        {
            var chat = new Chat(createChatDTO.Name);


            await _chatRepository.Add(chat);
        }

        public async Task<Chat> Find(FindChatDTO findChatDTO)
        {
            var chat = await _chatRepository.GetByName(findChatDTO.chatName);

            return chat;
        }
    }
}
