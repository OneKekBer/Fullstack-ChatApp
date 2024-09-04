using ChatApp.Business.Domains.User.Models;
using ChatApp.Data.Entities;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User> RegisterUser(RegisterDTO registerData);

        public Task<User> LogIn(RegisterDTO registerData);
    }
}
