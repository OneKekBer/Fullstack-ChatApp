using ChatApp.Business.Services.Interfaces;
using ChatApp.Data.Repository.Interfaces;
using ChatApp.Presentation.Domains.User;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Presentation.Controllers
{
    [ApiController()]
    [Route("api/auth/")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IChatRepository _chatRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService,IChatRepository chatRepository, IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _authService = authService;
            _chatRepository = chatRepository;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] AuthCredentialsDto dto)
        {
            if (dto.Password == string.Empty || dto.Login == string.Empty)
                return BadRequest(new
                {
                    Message = "Password or login are empty"
                });

            var result = await _authService.RegisterUser(dto.Login, dto.Password);

            return Ok(new { message = "User created successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthCredentialsDto dto)
        {
            if (dto.Password == "" || dto.Login == "") //pozor
                return BadRequest(new
                {
                    Message = "Password or login are empty"
                });

            var existingUser = await _authService.LogIn(dto.Login, dto.Password);
            var userChats = await _chatRepository.GetUserChats(existingUser.Id);
            var safeUserChats = userChats.Select(chat => new { chat.Id, chat.Name });

            return Ok(safeUserChats);
        }
    }
}
