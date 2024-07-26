using API.Domains.Chat.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : Controller, IChatController
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDTO createChatDTO)
        {
            await _chatService.CreateChat(createChatDTO);

            return Ok();
        }
    }
}
