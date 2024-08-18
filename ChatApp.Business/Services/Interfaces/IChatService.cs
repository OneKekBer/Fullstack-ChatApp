using ChatApp.Business.Domains.Chat.Models;
using ChatApp.Data.Entities;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IChatService
    {
        public Task Create(CreateChatDTO createChatDTO);

        public Task<IEnumerable<Chat>> Find(FindChatDTO findChatDTO);
    }
}