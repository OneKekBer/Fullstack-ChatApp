﻿using API.Database;
using API.Domains.User.Models;
using API.Exceptions.Auth;
using API.Models;
using API.Server;
using API.Services;
using Microsoft.AspNetCore.Mvc;


namespace EmptyAspMvcAuth.Controllers
{

    [ApiController()]
    [Route("api/auth/")]
    public class AuthController : Controller
    {
        private readonly UsersDB _userDatabase;
        private readonly ChatDB _chatDatabase;

        private readonly ChatGroupsService _groupService;
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UsersDB db, ChatDB chatDatabase, AuthService authService, ILogger<AuthController> logger, ChatGroupsService groupService)
        {
            _userDatabase = db;
            _chatDatabase = chatDatabase;
            _authService = authService;
            _logger = logger;
            _groupService = groupService;
            AddNewUser();
        }

        public void AddNewUser()
        {
            _userDatabase.Users.Add(new User("vital", AuthService.ConvertPasswordToHash("228")));
            _userDatabase.Users.Add(new User("ilya", AuthService.ConvertPasswordToHash("228")));
            _userDatabase.Users.Add(new User("anton", AuthService.ConvertPasswordToHash("228")));
            _userDatabase.Users.Add(new User("valera", AuthService.ConvertPasswordToHash("228")));

            _userDatabase.SaveChanges();
        }

        // in the future only admin can invoke this request
        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            _logger.LogInformation("getAll works");
            return Ok(_userDatabase.Users);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterDTO registerData)
        {
            if (registerData.Password == string.Empty || registerData.Login == string.Empty)
                throw new IncorrectCredentials();

            _authService.RegisterUser(registerData);

            return Ok(new { message = "User created successfully" });
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] RegisterDTO registerData)
        {
            if (registerData.Password == "" || registerData.Login == "")
                throw new IncorrectCredentials();

            var existingUser = _authService.LogIn(registerData);

            var userData = _groupService.GetUserGroups(existingUser); //all messages and all groups

            return Ok(userData);
        }
    }
}
