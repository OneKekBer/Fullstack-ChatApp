namespace API.Models
{
    public class Message
    {
        public Message() { }

        public Message(Guid authorId, Guid chatId, string text, string authorLogin)
        {
            AuthorId = authorId;
            ChatId = chatId;
            Text = text;
            CreatedAt = DateTime.UtcNow; // get current unix time
            AuthorLogin = authorLogin;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid ChatId { get; init; }

        public string AuthorLogin { get; init; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; init; }
    }

}
