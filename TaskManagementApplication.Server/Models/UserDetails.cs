namespace TaskManagementApplication.Server.Models
{
    /// <summary>
    /// Model to be used to send user data to the UI
    /// </summary>
    public class UserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string EmailId { get; set; }

        public string UserToken { get; set; }
    }
}
