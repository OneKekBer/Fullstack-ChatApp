namespace API.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("User is undefind") { }
    } 
}
