using API.Exceptions.ChatUI.Models;

namespace API.Exceptions.ChatUI
{
    public class ChatAlreadyExistsException : ChatException
    {
        public ChatAlreadyExistsException()
            : base("This chat already exists") { }
    }
}
