namespace API.Exceptions
{
    public class NotFoundInDatabaseException : Exception
    {
        public NotFoundInDatabaseException()
           : base("Smth in database is undefinded") { }
    }
}
