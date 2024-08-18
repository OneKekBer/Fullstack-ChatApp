using ChatApp.Data.Database;
using ChatApp.Data.Entities;
using ChatApp.Data.Exceptions;
using ChatApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDatabaseContext _appDatabase;

        public ChatRepository(AppDatabaseContext appDb)
        {
            _appDatabase = appDb;
        }

        public async Task Add(Chat entity)
        {
            var user = await GetByName(entity.Name);

            if (user is not null)
            {
                throw new AlreadyExistsException($"chat with name {entity.Name} already exists");
            }

            await _appDatabase.Chats.AddAsync(entity);

            await _appDatabase.SaveChangesAsync();
        }

        public async Task<Chat> GetById(Guid id)
        {
            var searchingGroup = await _appDatabase.Chats.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundInDatabaseException($"Cannot find group with id:{id}");

            return searchingGroup;
        }

        public async Task AddUserIdToChat(Chat chat, Guid userId)
        {
            var isContains = chat.UsersIds.Contains(userId);

            if (isContains)
                throw new Exception("user id already exists in chat");

            chat.UsersIds.Add(userId);
            await _appDatabase.SaveChangesAsync();
        }

        public async Task AddConnectionIdToChat(Chat chat, string connectionId)
        {
            chat.ConnectionIds.Add(connectionId);
            //_appDatabase.Update(chat);

            await _appDatabase.SaveChangesAsync();
        }


        public async Task<List<Chat>> GetUserChats(Guid userId)
        {
            var chats = await _appDatabase.Chats.Where(x => x.UsersIds.Contains(userId)).ToListAsync();

            return chats;
        }

        public Task Remove(Chat entity)
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetByName(string name)
        {
            var chat = _appDatabase.Chats.FirstOrDefaultAsync(x => x.Name == name) ?? throw new NotFoundInDatabaseException("chat repository", "get by name");

            return chat;
        }

        public async Task RemoveConnectionIdInAllChats(string connectionId) // Warning: maybe there will be bug, honestly idk))
        {
            var chats = await _appDatabase.Chats.Where(x => x.ConnectionIds.Contains(connectionId)).ToListAsync();

            foreach (Chat chat in chats)
            {
                chat.ConnectionIds.Remove(connectionId);
                _appDatabase.Update(chat);
            }

            await _appDatabase.SaveChangesAsync();
        }


        public async Task AddConnectionIdToAllUserChats(User user, string connectionId)
        {
            var chats = await _appDatabase.Chats
                .Where(x => x.UsersIds.Contains(user.Id))
                .ToListAsync();

            foreach (var chat in chats)
            {
                if (!chat.ConnectionIds.Contains(connectionId))
                    chat.ConnectionIds.Add(connectionId);
            }

            await _appDatabase.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            var chats = await _appDatabase.Chats.ToListAsync();

            return chats;
        }
    }
}
