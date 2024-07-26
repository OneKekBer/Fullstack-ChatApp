using API.Exceptions;
using API.Models;
using API.Server;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDatabase _usersDatabase;

        public UserRepository(UsersDatabase usersDB)
        {
            _usersDatabase = usersDB;
        }

        public async Task Add(User entity)
        {
            await _usersDatabase.Users.AddAsync(entity);

            await _usersDatabase.SaveChangesAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _usersDatabase.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundInDatabaseException();

            return user;
        }

        public async Task Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsLoginExists(string login)
        {
            var searchingLogin = await _usersDatabase.Users.FirstOrDefaultAsync(user => user.Login == login) ?? throw new NotImplementedException();

            return true;
        }

        public async Task<User> GetByLogin(string login)
        {
            var user = await _usersDatabase.Users.FirstOrDefaultAsync(x => x.Login == login) ?? throw new NotImplementedException();

            return user;
        }
    }
}
