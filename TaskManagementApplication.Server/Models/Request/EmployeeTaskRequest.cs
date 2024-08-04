using System.ComponentModel.DataAnnotations;

namespace TaskManagementApplication.Server.Models.Request
{
    /// <summary>
    /// Request model to create task
    /// </summary>
    public class EmployeeTaskRequest
    {
        public int Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int FinishedDays { get; set; }
    }
}
