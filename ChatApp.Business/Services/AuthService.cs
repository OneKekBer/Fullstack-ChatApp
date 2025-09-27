using ChatApp.Business.Helpers;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Data.Entities;
using ChatApp.Data.Repository.Interfaces;

namespace ChatApp.Business.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository { get; init; }

        public AuthService(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        public async Task<User> LogIn(string login, string password)
        {
            var user = await _userRepository.GetByLogin(login);

            if (!HashHelper.IsPasswordHashesEquals(user.PasswordHash, password))
                throw new Exception();

            return user;
        }

        public async Task<User> RegisterUser(string login, string password)
        {
            if (await _userRepository.IsLoginExists(login))
                throw new Exception();

            var createdUser = new User(login, HashHelper.ConvertPasswordToHash(password));
            await _userRepository.Add(createdUser);
            return createdUser;
        }
    }
}
