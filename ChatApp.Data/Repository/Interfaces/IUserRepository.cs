using ChatApp.Data.Entities;

namespace ChatApp.Data.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> IsLoginExists(string username);

        public Task<User> GetByLogin(string username);
    }
}
