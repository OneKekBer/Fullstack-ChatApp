

namespace API.Models
{
    public class Message
    {
        public Message() { }

        public Message(string text, User author)
        {
            Text = text;
            Author = author;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public string Text { get; set; } = string.Empty;

        public User Author { get; set; }
    }

}
