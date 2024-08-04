using TaskManagementApplication.Server.Core.Interfaces;
using TaskManagementApplication.Server.Domain;
using TaskManagementApplication.Server.Infrastructure;
using TaskManagementApplication.Server.Models;
using TaskManagementApplication.Server.Models.Request;

namespace TaskManagementApplication.Server.Core.Implementation
{
    [DefaultImplementation]
    public class TaskService : ITaskService
    {
        private IRepository<Domain.Task> _taskRepository;
        private IRepository<Status> _statusRepository;
        private IRepository<User> _userRepository;

        public TaskService(IRepository<Domain.Task> taskRepository, IRepository<Status> statusRepository,
        IRepository<User> userRepository)
        {
            _taskRepository = taskRepository;
            _statusRepository = statusRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Method to get task details by Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public EmployeeTaskDetails GetTaskDetails(int taskId)
        {
            var taskDetails = _taskRepository.Get(taskId);

            if (taskDetails != null)
            {
                return new EmployeeTaskDetails
                {
                    TaskId = taskDetails.Id,
                    TaskName = taskDetails.Name,
                    CreatedDate = taskDetails.CreatedDate,
                    FinishedDate = taskDetails.FinishDate,
                    TaskStatus = taskDetails.Name,
                    TaskStatusId = taskDetails.StatusId
                };
            }

            return null;
        }

        /// <summary>
        /// Returns list of tasks by user id
        /// </summary>
        public List<EmployeeTaskDetails> GetEmployeeTaskList(int userId)
        {
            var employeeTaskDetails = new List<EmployeeTaskDetails>();
            var userTaskLists = (from task in _taskRepository.Table
                                 join status in _statusRepository.Table on task.StatusId equals status.Id
                                 where task.UserId == userId
                                 select new { task, statusName = status.Name }).ToList();

            foreach (var userTask in userTaskLists)
            {
                var employeeTaskDetail = new EmployeeTaskDetails
                {
                    TaskId = userTask.task.Id,
                    TaskName = userTask.task.Name,
                    CreatedDate = userTask.task.CreatedDate,
                    FinishedDate = userTask.task.FinishDate,
                    TaskStatus = userTask.statusName
                };

                employeeTaskDetails.Add(employeeTaskDetail);
            }

            return employeeTaskDetails;
        }

        /// <summary>
        /// Method to get manager's employee task list
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public List<EmployeeTaskDetails> GetAllEmployeesTaskByManagerId(int managerId)
        {
            var employeeTaskDetails = new List<EmployeeTaskDetails>();
            var taskLists = (from task in _taskRepository.Table
                             join status in _statusRepository.Table on task.StatusId equals status.Id
                             join user in _userRepository.Table on task.UserId equals user.Id
                             where user.ManagerId == managerId
                             select new { task, statusName = status.Name, firstName = user.FirstName, lastName = user.LastName }).ToList();

            foreach (var task in taskLists)
            {
                var employeeTaskDetail = new EmployeeTaskDetails
                {
                    TaskId = task.task.Id,
                    TaskName = task.task.Name,
                    CreatedDate = task.task.CreatedDate,
                    FinishedDate = task.task.FinishDate,
                    TaskStatus = task.statusName,
                    UserName = task.firstName + "" + task.lastName
                };

                employeeTaskDetails.Add(employeeTaskDetail);
            }

            return employeeTaskDetails;
        }

        /// <summary>
        /// Method to get the employee task details for admin
        /// </summary>
        /// <returns></returns>
        public List<EmployeeTaskDetails> GetMasterTaskList()
        {
            var employeeTaskDetails = new List<EmployeeTaskDetails>();
            var taskLists = (from task in _taskRepository.Table
                             join status in _statusRepository.Table on task.StatusId equals status.Id
                             join user in _userRepository.Table on task.UserId equals user.Id
                             select new { task, statusName = status.Name, firstName = user.FirstName, lastName = user.LastName }).ToList();
            foreach (var task in taskLists)
            {
                var employeeTaskDetail = new EmployeeTaskDetails
                {
                    TaskId = task.task.Id,
                    TaskName = task.task.Name,
                    CreatedDate = task.task.CreatedDate,
                    FinishedDate = task.task.FinishDate,
                    TaskStatus = task.statusName,
                    UserName = task.firstName + "" + task.lastName
                };

                employeeTaskDetails.Add(employeeTaskDetail);
            }

            return employeeTaskDetails;
        }

        /// <summary>
        /// Method to update task details
        /// </summary>
        /// <param name="request"></param>
        public bool UpsertTask(EmployeeTaskRequest request)
        {
            try
            {
                var taskDetails = _taskRepository.Get(request.Id);
                if (taskDetails != null)
                {
                    taskDetails.StatusId = request.StatusId;
                    taskDetails.ModifiedOn = DateTime.Now;
                    taskDetails.IsNew = false;

                    _taskRepository.Save(taskDetails);
                }
                else
                {
                    var task = new Domain.Task
                    {
                        Name = request.TaskName,
                        StatusId = 1,
                        UserId = request.UserId,
                        FinishDate = DateTime.UtcNow.AddDays(request.FinishedDays),
                        CreatedDate = DateTime.UtcNow,
                        IsNew = true
                    };

                    _taskRepository.Save(task);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
