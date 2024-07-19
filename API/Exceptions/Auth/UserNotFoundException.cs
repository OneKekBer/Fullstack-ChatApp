using API.Exceptions.Auth.Models;

namespace API.Exceptions.Auth
{
    public class UserNotFoundException : AuthException
    {
        public UserNotFoundException()
            : base("Login or password are incorrect") { }
    }
}