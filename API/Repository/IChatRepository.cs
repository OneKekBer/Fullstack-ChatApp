using API.Models;

namespace API.Repository
{
    public interface IChatRepository : IRepository<Chat>
    {
        public Task GetUserGroupsByLogin(string login);
    }
}
