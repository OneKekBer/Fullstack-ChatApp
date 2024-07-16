namespace API.Models
{
    public class User
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Login { get; init; } = string.Empty;

        public string PasswordHash { get; init; } = string.Empty;

        public List<Guid> AllGroupsId { get; set; } = new List<Guid>();

        public string ConnectionId { get; set; } = string.Empty;

        public User(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }
    }
}
