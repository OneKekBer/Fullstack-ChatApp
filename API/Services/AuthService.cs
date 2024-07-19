using API.Database;
using API.Domains.User.Models;
using API.Exceptions;
using API.Exceptions.Auth;
using API.Models;
using API.Server;
using System.Security.Cryptography;
using System.Text;


namespace API.Services
{
    public class AuthService
    {
        private UsersDB _userDatabase { get; init; }
        private ChatDB _chatDatabase { get; init; }


        public AuthService(UsersDB userDatabase, ChatDB chatDatabase)
        {
            _userDatabase = userDatabase;
            _chatDatabase = chatDatabase;
        }

        public static string ConvertPasswordToHash(string password)
        {
            return Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }

        private static bool IsPasswordHashesEquals(User user, string incomePassword)
        {
            if (user.PasswordHash == ConvertPasswordToHash(incomePassword))
                return true; // Don't forget about spaces between code-blocks
            return false;
        }

        public User LogIn(RegisterDTO registerData)
        {
            var user = _userDatabase.FindUserByLogin(registerData.Login);

            if (!IsPasswordHashesEquals(user, registerData.Password))
                throw new IncorrectCredentials();

            return user;
        }

        public User RegisterUser(RegisterDTO registerData)
        {
            if (_userDatabase.IsLoginExists(registerData.Login))
                throw new LoginAlreadyUseException();

            var createdUser = new User(registerData.Login, ConvertPasswordToHash(registerData.Password));
            _userDatabase.Users.Add(createdUser);
            _userDatabase.SaveChanges();
            return createdUser;
        }
    }
}
