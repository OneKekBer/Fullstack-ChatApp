namespace ChatApp.Data.Exceptions
{
    public class NotFoundInDatabaseException : Exception
    {
        public NotFoundInDatabaseException(string message)
        : base(message) { }

        public NotFoundInDatabaseException(string source, string entity)
           : base($"aIn database {entity} is undefinded in {source}") { }
    }
}
