using API.Domains.User.Models;
using API.Models;

namespace API.Services
{
    public interface IAuthService
    {
        public Task<User> RegisterUser(RegisterDTO registerData);

        public Task<User> LogIn(RegisterDTO registerData);
    }
}
