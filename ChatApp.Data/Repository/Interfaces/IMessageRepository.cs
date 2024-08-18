using ChatApp.Data.Entities;

namespace ChatApp.Data.Repository.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        public Task<IEnumerable<Message>> GetChatMessages(Guid chatId);
    }
}
