using API.Domains.Chat.Models;
using API.Models;

namespace API.Services
{
    public interface IChatService
    {
        public Task Create(CreateChatDTO createChatDTO);

        public Task<IEnumerable<Chat>> Find(FindChatDTO findChatDTO);
    }
}