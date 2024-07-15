namespace API.Models
{
    public class ChatGroup
    {
        public Guid Id { get; set; } = Guid.Empty;

        public List<User> Users { get; set; } = new List<User>();

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}


