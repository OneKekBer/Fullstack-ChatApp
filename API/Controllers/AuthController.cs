using API.Database;
using API.Domains.User.Models;
using API.Models;
using API.Repository;
using API.Services;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace EmptyAspMvcAuth.Controllers
{

    [ApiController()]
    [Route("api/auth/")]
    public class AuthController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, UserRepository userRepository, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _authService = authService;
            _logger = logger;
            AddNewUser();
        }

        public async Task AddNewUser()
        {
            await _userRepository.Add(new User("vital", HashHelper.ConvertPasswordToHash("228")));
            await _userRepository.Add(new User("ilya", HashHelper.ConvertPasswordToHash("228")));
            await _userRepository.Add(new User("anton", HashHelper.ConvertPasswordToHash("228")));
            await _userRepository.Add(new User("valera", HashHelper.ConvertPasswordToHash("228")));
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

            return Ok();
        }
    }
}
