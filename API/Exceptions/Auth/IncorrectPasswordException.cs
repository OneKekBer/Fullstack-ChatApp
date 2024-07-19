using API.Exceptions.Auth.Models;

namespace API.Exceptions.Auth
{
    public class IncorrectCredentials : AuthException
    {
        public IncorrectCredentials()
            : base("Password or login are incorrect") { }
    }

}
