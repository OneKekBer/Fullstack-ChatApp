using ChatApp.Business.Services;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Presentation.Domains.Chat;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Presentation.Controllers
{
    [ApiController()]
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly ChatHubService  _chatHubService;
        private readonly ILogger<ChatController> _logger;
    
        public ChatController(IChatService chatService, ILogger<ChatController> logger, ChatHubService chatHubService)
        {
            _chatService = chatService;
            _chatHubService = chatHubService;
            _logger = logger;
        }

        public record UploadImageDto(IFormFile Image);

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDto dto)
        {
            await _chatHubService.Upload(dto.Image);
            return Ok();
        }
        
        [HttpPost("find")]
        public async Task<IActionResult> Find([FromBody] FindChatDto dto)
        {
            _logger.LogInformation("chat post controller");
            var references = await _chatService.Find(dto.Name);

            if(references is null)
                return NotFound(new {Message = "Chat not found"});

            return Ok(references);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChatDto dto)
        {
            _logger.LogInformation("chat post controller");
            await _chatService.Create(dto.Title);

            return Ok();
        }
    }
}
