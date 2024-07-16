namespace API.Exceptions
{
    public class UserNotFoundException : AuthException
    {
        public UserNotFoundException()
            : base("Login or password are incorrect") { }
    } 
}
