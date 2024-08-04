using TaskManagementApplication.Server.Models;
using TaskManagementApplication.Server.Models.Request;

namespace TaskManagementApplication.Server.Core.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Method to validate a user before hitting any api.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetUserTokenDetails(string token);
        /// <summary>
        /// Method to create a user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool CreateUser(CreateUserRequest request);
        /// <summary>
        /// Method to update a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool UpdateUserDetail(UpdateUserRequest request);
        /// <summary>
        /// Method to get drop down list of managers available.
        /// </summary>
        /// <returns></returns>
        List<ManagerDetailModel> GetManagerDetailList();
    }
}
