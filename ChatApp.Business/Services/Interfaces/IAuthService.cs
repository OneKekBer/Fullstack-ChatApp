using ChatApp.Data.Entities;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User> RegisterUser(string login, string password);
        public Task<User> LogIn(string login, string password);
    }
}
