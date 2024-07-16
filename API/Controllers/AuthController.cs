using API.Domains.User.Models;
using API.Server;
using API.Services;
using Microsoft.AspNetCore.Mvc;


namespace EmptyAspMvcAuth.Controllers
{

    [ApiController()]
    [Route("api/auth/")]
    public class AuthController : Controller
    {
        private readonly UsersDB _db;
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UsersDB db, AuthService authService, ILogger<AuthController> logger)
        {
            _db = db;
            _authService = authService;
            _logger = logger;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            _logger.LogInformation("getAll works");
            return Ok(_db.Users);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterDTO registerData)
        {
            _authService.RegisterUser(registerData);

            return Ok("user create successfully");
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] RegisterDTO registerData)
        {
            var existingUser = _authService.LogIn(registerData);

            return Ok($"You loged in successfully your id: {existingUser.Id}");
        }
    }
}
