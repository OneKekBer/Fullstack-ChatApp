using API.Database;
using API.Domains.User.Models;
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

        

        public List<User> GetOnlineUsers()
        {
            return Users.Where((x) => x.Status == UserStatus.Online).ToList();
        }

        public List<OnlineUserData> FormatUserDataToOnlineUserData(IEnumerable<User> onlineUsers)
        {
            return onlineUsers
                .Select(u => new OnlineUserData(u.Login, u.ConnectionId))
                .ToList();
        }

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

        public User FindUserByConnectionId(string connectionId)
        {
            var searchingUser = Users.FirstOrDefault(user => user.ConnectionId == connectionId);

            if (searchingUser is null)
                throw new UserNotFoundException();

            return searchingUser;
        }
    }
}
