using API.Models;

namespace API.Repository
{
    public interface IChatRepository : IRepository<Chat>
    {
        public Task<List<Chat>> GetUserChats(Guid id);

        public Task<Chat> GetByName(string name);

        public Task AddConnectionIdToChat(Chat chat, string ConnectionId);
        public Task AddUserIdToChat(Chat chat, Guid userId);
        public Task RemoveConnectionIdInAllChats(string connectionId);

        public Task AddConnectionIdToAllUserChats(User user,string connectionId);
    }
}
