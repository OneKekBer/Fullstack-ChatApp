using ChatApp.Business.Domains.User.Models;
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

        public async Task<User> LogIn(RegisterDTO registerData)
        {
            var user = await _userRepository.GetByLogin(registerData.Login);

            if (!HashHelper.IsPasswordHashesEquals(user.PasswordHash, registerData.Password))
                throw new Exception();

            return user;
        }

        public async Task<User> RegisterUser(RegisterDTO registerData)
        {
            if (await _userRepository.IsLoginExists(registerData.Login))
                throw new Exception();

            var createdUser = new User(registerData.Login, HashHelper.ConvertPasswordToHash(registerData.Password));
            await _userRepository.Add(createdUser);
            return createdUser;
        }
    }
}
