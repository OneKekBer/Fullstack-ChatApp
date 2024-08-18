using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Data.Entities.Safe
{
    public class SafeChat
    {
        public SafeChat() { }

        public SafeChat(Guid id, string chatName) 
        {
            Id = id;
            ChatName = chatName;
        }

        public Guid Id {  get; init; }

        public string ChatName { get; init; }
    }
}
