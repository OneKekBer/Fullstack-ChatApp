namespace API.Models
{
    public class Message
    {
        public Message() { }

        public Message(Guid authorId, Guid chatId, string text)
        {
            AuthorId = authorId;
            ChatId = chatId;
            Text = text;
            CreatedAt = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds(); // get current unix time
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid ChatId { get; init; }

        public Guid AuthorId { get; set; }

        public string Text { get; set; } = string.Empty;

        public long CreatedAt { get; init; }
    }

}
