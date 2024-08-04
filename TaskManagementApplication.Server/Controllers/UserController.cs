using Microsoft.AspNetCore.Mvc;
using TaskManagementApplication.Server.Core.Interfaces;
using TaskManagementApplication.Server.Infrastructure.Authorization;
using TaskManagementApplication.Server.Models.Request;

namespace TaskManagementApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            var isCreated = _userService.CreateUser(request);

            if (isCreated) { return Ok(); }
            else { return BadRequest("Issue in creating a user."); }
        }

        [HttpPost]
        [Route("updateUser")]
        public IActionResult UpdateUserDetail(UpdateUserRequest request)
        {
            var isUpdated = _userService.UpdateUserDetail(request);

            if (isUpdated) { return Ok(); }
            else { return BadRequest("Issue in creating a user"); }
        }

        [HttpGet]
        [Route("getManagerList")]
        public IActionResult GetAllManagerDetails()
        {
            return Ok(_userService.GetManagerDetailList());
        }
    }
}
