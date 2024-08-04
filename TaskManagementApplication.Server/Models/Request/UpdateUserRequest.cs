namespace TaskManagementApplication.Server.Models.Request
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
