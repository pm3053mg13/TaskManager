using TaskManagementApplication.Server.Models;
using TaskManagementApplication.Server.Models.Request;

namespace TaskManagementApplication.Server.Core.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Method to get task details by Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        EmployeeTaskDetails GetTaskDetails(int taskId);

        /// <summary>
        /// Returns list of tasks by user id
        /// </summary>
        List<EmployeeTaskDetails> GetEmployeeTaskList(int userId);

        /// <summary>
        /// Method to get manager's employee task list
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        List<EmployeeTaskDetails> GetAllEmployeesTaskByManagerId(int managerId);

        /// <summary>
        /// Method to get the employee task details for admin
        /// </summary>
        /// <returns></returns>
        List<EmployeeTaskDetails> GetMasterTaskList();

        /// <summary>
        /// Method to update task details
        /// </summary>
        /// <param name="request"></param>
        bool UpsertTask(EmployeeTaskRequest request);
    }
}
