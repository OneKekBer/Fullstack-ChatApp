namespace API.Models
{
    public class ChatGroup
    {
        public ChatGroup()
        {
        }

        public ChatGroup(List<UserSafeData> users, List<Message> messages, string name)
        {
            Users = users;
            Messages = messages;
            Name = name;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public List<UserSafeData> Users { get; set; } = new List<UserSafeData>();

        public List<Message> Messages { get; set; } = new List<Message>();

        public string Name { get; init; } = string.Empty;

        
    }
}


