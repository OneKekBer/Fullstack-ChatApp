using API.Database;
using API.Exceptions;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDatabase _chatsDatabase;

        public ChatRepository(ChatDatabase chatDB)
        {
            _chatsDatabase = chatDB;
        }

        public async Task Add(Chat entiy)
        {
            await _chatsDatabase.Chats.AddAsync(entiy);

            await _chatsDatabase.SaveChangesAsync();
        }

        public async Task<Chat> GetById(Guid id)
        {
            var searchingGroup = await _chatsDatabase.Chats.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundInDatabaseException();

            return searchingGroup;
        }

        public async Task AddConnectionIdToChat(Chat chat, string ConnectionId) // Question: is it correct ?? should i find by id chat and then use it in method
        {
            chat.ConnectionId.Add(ConnectionId);
            _chatsDatabase.Update(chat);

            await _chatsDatabase.SaveChangesAsync();
        }

        public async Task RemoveConnectionIdInAllChats(string connectionId) // Warning: maybe there will be bug, honestly idk))
        {
            var chats = await _chatsDatabase.Chats.Where(x=> x.ConnectionId.Contains(connectionId)).ToListAsync();

            foreach (Chat chat in chats)
            {
                chat.ConnectionId.Remove(connectionId);
                _chatsDatabase.Update(chat);
            }

            await _chatsDatabase.SaveChangesAsync();
        }

        public async Task<List<Chat>> GetUserChats(Guid id)
        {
            var chats = await _chatsDatabase.Chats.Where(x => x.UsersId.Contains(id)).ToListAsync();  

            return chats;
        }

        public Task Remove(Chat entity)
        {
            throw new NotImplementedException();
        }
    }
}
