using API.Models;

namespace API.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T entiy);

        public Task RemoveById(Guid id);

        public Task<T> GetById(Guid id);
    }
}
