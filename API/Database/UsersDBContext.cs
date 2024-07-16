using API.Database;
using API.Exceptions;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Server
{
    public class UsersDB : DbContext
    {
        public UsersDB(DbContextOptions<UsersDB> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public bool IsLoginExists(string login)
        {
            var searchingLogin = Users.FirstOrDefault(user => user.Login == login);

            if (searchingLogin is null)
                return false;

            return true;
        }

        public User FindUserByLogin(string login)
        {
            var searchingUser = Users.FirstOrDefault(user => user.Login == login);

            if (searchingUser is null)
                throw new UserNotFoundException();

            return searchingUser;
        }
    }
}
