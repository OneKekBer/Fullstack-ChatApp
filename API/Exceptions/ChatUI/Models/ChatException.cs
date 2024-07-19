namespace API.Exceptions.ChatUI.Models
{
    public abstract class ChatException : Exception
    {
        public ChatException(string message) : base(message) { }

    }
}
