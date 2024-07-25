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

        public Task GetUserGroupsByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public Task RemoveById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
