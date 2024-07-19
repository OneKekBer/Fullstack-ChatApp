

namespace API.Models
{
    public class Message
    {
        public Message() { }

        public Message(UserSafeData author, string text)
        {
            Author = author;
            Text = text;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public string Text { get; set; } = string.Empty;

        public UserSafeData Author { get; set; }
    }

}
