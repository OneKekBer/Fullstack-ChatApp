using API.Models;

namespace API.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T entity);

        public Task Remove(T entity);

        public Task<T> GetById(Guid id);

        // plus delete
    }
}
