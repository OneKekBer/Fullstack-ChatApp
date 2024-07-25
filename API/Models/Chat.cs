namespace API.Models
{
    public class Chat
    {
        public Chat()
        {
        }

        public Chat(string name)
        {
            Name = name;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public List<Guid> UsersId { get; set; } = new List<Guid>();

        public List<string> ContextId { get; set; } = new List<string>();

        public string Name { get; init; } = string.Empty;
    }
}


