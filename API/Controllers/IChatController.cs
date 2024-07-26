using API.Domains.Chat.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    internal interface IChatController
    {
        public Task<IActionResult> CreateChat([FromBody] CreateChatDTO createChatDTO);
    }
}