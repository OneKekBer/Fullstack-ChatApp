namespace API.Models
{
    public class ChatGroup
    {
        public ChatGroup()
        {
        }

        public ChatGroup(List<User> users, List<Message> messages, string name)
        {
            Users = users;
            Messages = messages;
            Name = name;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public List<User> Users { get; set; } = new List<User>();

        public List<Message> Messages { get; set; } = new List<Message>();

        public string Name { get; init; } = string.Empty;

        
    }
}


