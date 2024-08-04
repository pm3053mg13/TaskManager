using TaskManagementApplication.Server.Models;

namespace TaskManagementApplication.Server.Core.Interfaces
{
    /// <summary>
    /// Interface to be used for login service
    /// </summary>
    public interface ILoginService
    {
        UserDetails ValidateUser(string username, string password);
    }
}
