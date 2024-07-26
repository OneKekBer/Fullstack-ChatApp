using API.Domains.Chat.Models;

namespace API.Services
{
    public interface IChatService
    {
        public Task CreateChat(CreateChatDTO createChatDTO);
    }
}