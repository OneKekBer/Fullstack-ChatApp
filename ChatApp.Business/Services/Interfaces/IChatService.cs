using ChatApp.Data.Entities;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IChatService
    {
        public Task Create(string title);
        public Task<IEnumerable<Chat>> Find(string title);
    }
}