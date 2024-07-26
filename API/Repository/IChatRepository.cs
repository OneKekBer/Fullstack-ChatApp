using API.Models;

namespace API.Repository
{
    public interface IChatRepository : IRepository<Chat>
    {
        public Task<List<Chat>> GetUserChats(Guid id);

        public Task AddConnectionIdToChat(Chat chat, string ConnectionId);

        public Task RemoveConnectionIdInAllChats(string connectionId);
    }
}
