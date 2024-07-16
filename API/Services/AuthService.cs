using API.Domains.User.Models;
using API.Exceptions;
using API.Models;
using API.Server;
using System.Security.Cryptography;
using System.Text;


namespace API.Services
{
    public class AuthService
    {
        private UsersDB _database { get; init; }

        public AuthService(UsersDB dataBase)
        {
            _database = dataBase;
        }

        private static string ConvertPasswordToHash(string password)
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
            var user = _database.FindUserByLogin(registerData.Login);

            if (!IsPasswordHashesEquals(user, registerData.Password))
                throw new IncorrectPasswordException();

            return user;
        }

        public User RegisterUser(RegisterDTO registerData)
        {
            if (_database.IsLoginExists(registerData.Login))
                throw new Exception("Login already used");

            var createdUser = new User(registerData.Login, ConvertPasswordToHash(registerData.Password));
            _database.Users.Add(createdUser);
            _database.SaveChanges();
            return createdUser;
        }
    }
}
