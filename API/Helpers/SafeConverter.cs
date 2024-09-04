using ChatApp.Data.Entities;
using ChatApp.Data.Entities.Safe;

namespace API.Helpers
{
    public class SafeConverter
    {
        public static IEnumerable<SafeChat> ChatToSafeConverter(IEnumerable<Chat> chats)
        {
            foreach(var chat in chats)
            {
                yield return new SafeChat(chat.Id, chat.Name);
            }
        }
    }
}
