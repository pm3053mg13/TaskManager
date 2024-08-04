namespace TaskManagementApplication.Server.Models.Request
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ManagerId { get; set; }
        public int RoleId { get; set; }
    }
}
