namespace API.Models
{
    public class UserSafeData
    {
        public UserSafeData(string login, string connectionId)
        {
            Login = login;
            ConnectionId = connectionId;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public string Login { get; init; } = string.Empty;

        public string ConnectionId { get; init; } = string.Empty;


    }
}
