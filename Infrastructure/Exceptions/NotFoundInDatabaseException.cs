namespace API.Exceptions
{
    public class NotFoundInDatabaseException : Exception
    {
        public NotFoundInDatabaseException()
           : base("Smth in database is undefinded") { }

        public NotFoundInDatabaseException(string source, string entity)
           : base($"aIn database {entity} is undefinded in {source}") { }
    }
}
