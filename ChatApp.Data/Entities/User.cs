namespace ChatApp.Data.Entities
{
    public class User
    {
        public User() { }

        public User(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public string Login { get; init; } = string.Empty;

        public string PasswordHash { get; init; } = string.Empty;        
    }
}
