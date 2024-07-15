namespace API.Models
{
    public class Message
    {
        public Message(string text, User user)
        {
            Text = text;
            User = user;
        }

        public string Text { get; set; } = string.Empty;

        public User User { get; set; }
    }

}
