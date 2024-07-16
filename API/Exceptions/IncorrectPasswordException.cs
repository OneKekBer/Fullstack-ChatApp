namespace API.Exceptions
{
    public class IncorrectPasswordException : AuthException
    {
        public IncorrectPasswordException()
            : base("Password is incorrect") { }
    }

}
