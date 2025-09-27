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

        public async Task Create(string title)
        {
            var chat = new Chat(title);
            await _chatRepository.Add(chat);
        }

        public async Task<IEnumerable<Chat>> Find(string title)
        {
            var chats = await _chatRepository.GetAll();

            var refs = chats.Where((x) => x.Name.Contains(title)).Take(10).ToList();

            return refs;
        }
    }
}
