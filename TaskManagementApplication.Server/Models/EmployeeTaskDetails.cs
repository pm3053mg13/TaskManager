namespace TaskManagementApplication.Server.Models
{
    public class EmployeeTaskDetails
    {
        public int TaskId { get; set; }
        public string UserName { get; set; }
        public string TaskName { get; set; }
        public string TaskStatus { get; set; }
        public int TaskStatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}
