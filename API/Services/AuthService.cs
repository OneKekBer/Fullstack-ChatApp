using API.Domains.User.Models;
using API.Models;
using API.Repository;
using Infrastructure.Helpers;


namespace API.Services
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
