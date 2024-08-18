using ChatApp.Business.Domains.Chat.Models;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Data.Entities;
using ChatApp.Data.Repository.Interfaces;

namespace ChatApp.Business.Services
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
