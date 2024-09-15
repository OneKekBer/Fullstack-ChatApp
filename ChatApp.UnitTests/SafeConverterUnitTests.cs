using API.Helpers;
using ChatApp.Data.Entities;
using ChatApp.Data.Entities.Safe;

namespace ChatApp.UnitTests
{
    public class SafeConverterUnitTests
    {
        //framework - triple A
        [Fact]
        public void ChatToSafeConverter_WhenChatWithValidDataExists_ChatConvertsToSafeChat()
        {
            // Arrange 
            var chat = new Chat() 
            { 
                Name = "Chat",
                ConnectionIds = new List<string>() {"safdq132" },
                Id = Guid.NewGuid(),
                UsersIds=new List<Guid>() { Guid.NewGuid() } 
            };

            var expectedSafeChat = new SafeChat() { ChatName = chat.Name, Id = chat.Id };
            //new List<SafeChat> { expectedSafeChat }
            var chats = new List<Chat>() { chat };

            // Act

            var result = SafeConverter.ChatToSafeConverter(chats);

            // Assert

            Assert.Collection(result, item => 
            {
                Assert.Equal(item.ChatName, expectedSafeChat.ChatName);
                Assert.Equal(item.Id, expectedSafeChat.Id);
            });
        }

        [Fact]
        public void ChatToSafeConverter_WhenChatsIsEmpty_ChatReturnsEmptyCollections()
        {
            // Arrange 
            var chats = new List<Chat>() { };

            // Act

            var result = SafeConverter.ChatToSafeConverter(chats);

            // Assert

            Assert.Empty(result);

        }
    }
}