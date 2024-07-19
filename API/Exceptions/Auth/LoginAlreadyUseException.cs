using API.Exceptions.Auth.Models;

namespace API.Exceptions.Auth
{
    public class LoginAlreadyUseException : AuthException
    {
        public LoginAlreadyUseException()
            : base("Login already used") { }
    }
}
