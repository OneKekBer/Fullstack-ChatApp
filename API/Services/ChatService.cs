
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

        public async Task<IEnumerable<Chat>> Find(FindChatDTO findChatDTO)
        {
            var chats = await _chatRepository.GetAll();
            
            var refs = chats.Where((x) => x.Name.Contains(findChatDTO.chatName)).Take(10).ToList();

            return refs;
        }
    }
}
