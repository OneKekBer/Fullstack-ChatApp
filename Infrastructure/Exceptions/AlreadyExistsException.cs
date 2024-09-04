namespace Infrastructure.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string text)
           : base(text) { }
    }
}
