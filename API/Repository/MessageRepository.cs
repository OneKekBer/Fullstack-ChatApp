using API.Exceptions;
using API.Models;
using API.Server;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessagesDatabase _messagesDatabase;

        public MessageRepository(MessagesDatabase messagesDatabase)
        {
            _messagesDatabase = messagesDatabase;
        }

        public async Task Add(Message entiy)
        {
            await _messagesDatabase.Messages.AddAsync(entiy);

            await _messagesDatabase.SaveChangesAsync();
        }

        public async Task<Message> GetById(Guid id)
        {
            var message = await _messagesDatabase.Messages.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundInDatabaseException();

            return message;
        }

        public async Task<IEnumerable<Message>> GetChatMessages(Guid chatId)
        {
            var messages = await _messagesDatabase.Messages
                .Where(x => x.ChatId == chatId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            return messages;
        }

        public async Task Remove(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
