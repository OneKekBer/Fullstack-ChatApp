using API.Models;

namespace API.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> IsLoginExists(string username);

        public Task<User> GetByLogin(string username);
    }
}
