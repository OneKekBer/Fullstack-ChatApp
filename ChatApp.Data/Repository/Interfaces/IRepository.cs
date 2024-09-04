namespace ChatApp.Data.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T entity);

        public Task Remove(T entity);

        public Task<T> GetById(Guid id);

        public Task<IEnumerable<T>> GetAll();

        // plus delete
    }
}
