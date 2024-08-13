using API.Database;
using API.Exceptions;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDatabaseContext _appDatabase;

        public UserRepository(AppDatabaseContext appDb)
        {
            _appDatabase = appDb;
        }

        public async Task Add(User entity)
        {
            await _appDatabase.Users.AddAsync(entity);

            await _appDatabase.SaveChangesAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _appDatabase.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundInDatabaseException("");

            return user;
        }

        public async Task Remove(User entity)
        {
            throw new NotImplementedException();
        }

            public async Task<bool> IsLoginExists(string login)
            {
            var searchingLogin = await _appDatabase.Users.FirstOrDefaultAsync(user => user.Login == login);

                if(searchingLogin is not null)
                    return true;
                return false;
            }

        public async Task<User> GetByLogin(string login)
        {
            var user = await _appDatabase.Users.FirstOrDefaultAsync(x => x.Login == login) ?? throw new NotFoundInDatabaseException("user repository", "GetByLogin");

            return user;
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
