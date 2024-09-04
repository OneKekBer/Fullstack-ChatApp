using ChatApp.Data.Database;
using ChatApp.Data.Entities;
using ChatApp.Data.Exceptions;
using ChatApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDatabaseContext _appDatabase;

        public MessageRepository(AppDatabaseContext appDb)
        {
            _appDatabase = appDb;
        }

        public async Task Add(Message entity)
        {
            await _appDatabase.Messages.AddAsync(entity);

            await _appDatabase.SaveChangesAsync();
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Message> GetById(Guid id)
        {
            var message = await _appDatabase.Messages.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundInDatabaseException($"Cannot find message with id: {id}");

            return message;
        }

        public async Task<IEnumerable<Message>> GetChatMessages(Guid chatId)
        {
            var messages = await _appDatabase.Messages
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
