using API.Helpers;
using ChatApp.Business.Domains.User.Models;
using ChatApp.Business.Services.Interfaces;
using ChatApp.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerData)
        {
            if (registerData.Password == string.Empty || registerData.Login == string.Empty)
                return BadRequest(new
                {
                    Message = "Password or login are empty"
                });

            await _authService.RegisterUser(registerData);

            return Ok(new { message = "User created successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] RegisterDTO registerData)
        {
            if (registerData.Password == "" || registerData.Login == "")
                return BadRequest(new
                {
                    Message = "Password or login are empty"
                });

            var existingUser = await _authService.LogIn(registerData);

            var userChats = await _chatRepository.GetUserChats(existingUser.Id);
            
            var safeUserChats = SafeConverter.ChatToSafeConverter(userChats); // Question: does this method should be async???

            return Ok(safeUserChats);
        }
    }
}
