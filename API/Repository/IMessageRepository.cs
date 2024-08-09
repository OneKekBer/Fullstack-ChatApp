using API.Models;

namespace API.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        public Task<IEnumerable<Message>> GetChatMessages(Guid chatId);
    }
}
