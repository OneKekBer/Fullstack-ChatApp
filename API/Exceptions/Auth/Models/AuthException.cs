namespace API.Exceptions.Auth.Models
{
    public abstract class AuthException : Exception
    {
        public AuthException(string message) : base(message) { }

    }
}
