namespace API.Exceptions
{
    public class LoginAlreadyUseException : AuthException
    {
        public LoginAlreadyUseException()
            : base("Login already used") { }
    }
}
