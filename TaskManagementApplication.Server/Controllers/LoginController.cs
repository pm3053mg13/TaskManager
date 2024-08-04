using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApplication.Server.Core.Interfaces;
using TaskManagementApplication.Server.Models.Request;

namespace TaskManagementApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("loginUser")]
        public IActionResult ValidateUser(UserLoginRequest userLoginRequest)
        {
            var userDetails = _loginService.ValidateUser(userLoginRequest.UserName, userLoginRequest.Password);

            if (userDetails != null) { return Ok(userDetails); }
            else { return BadRequest("No Data Found"); }
        }
    }
}
