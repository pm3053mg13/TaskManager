using Microsoft.AspNetCore.Mvc;
using TaskManagementApplication.Server.Core.Interfaces;
using TaskManagementApplication.Server.Infrastructure.Authorization;
using TaskManagementApplication.Server.Models.Request;

namespace TaskManagementApplication.Server.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("getTaskDetails/{taskId}")]
        public IActionResult GetTaskDetails(int taskId)
        {
            var taskDetais = _taskService.GetTaskDetails(taskId);
            if (taskDetais != null)
            {
                return Ok(_taskService.GetTaskDetails(taskId));
            }
            else return Ok("No Data Found");
        }

        [HttpGet]
        [Route("getEmployeeTask/{userId}")]
        public IActionResult GetEmployeeTaskList(int userId)
        {
            return Ok(_taskService.GetEmployeeTaskList(userId));
        }

        [HttpGet]
        [Route("getTask/{managerId}")]
        public IActionResult GetEmployeeTaskListByManagerId([FromRoute] int managerId)
        {
            return Ok(_taskService.GetAllEmployeesTaskByManagerId(managerId));
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetMasterTaskList()
        {
            return Ok(_taskService.GetMasterTaskList());
        }

        [HttpPost]
        [Route("upsert")]
        public IActionResult Upsert(EmployeeTaskRequest request)
        {
            var isUpdated = _taskService.UpsertTask(request);

            if (isUpdated) return Ok(isUpdated);
            else return BadRequest(isUpdated);
        }
    }
}
