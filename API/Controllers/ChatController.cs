using API.Domains.Chat.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController()]
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpPost("find")]
        public async Task<IActionResult> Find([FromBody] FindChatDTO findChatDTO)
        {
            _logger.LogInformation("chat post controller");
            var references = await _chatService.Find(findChatDTO);

            if(references is null)
                return NotFound(new {Message = "Chat not found"});

            return Ok(references);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChatDTO createChatDTO)
        {
            _logger.LogInformation("chat post controller");
            await _chatService.Create(createChatDTO);

            return Ok();
        }
    }
}
