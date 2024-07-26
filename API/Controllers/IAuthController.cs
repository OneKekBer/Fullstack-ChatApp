using API.Domains.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public interface IAuthController
    {
        public Task<ActionResult> Register([FromBody] RegisterDTO registerData);

        public Task<ActionResult> Login([FromBody] RegisterDTO registerData);
    }
}
