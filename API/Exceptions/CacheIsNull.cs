namespace API.Exceptions
{
    public class CacheIsNull : Exception
    {
        public CacheIsNull()
            : base("There are no this user in the cache") { }
    }

}
