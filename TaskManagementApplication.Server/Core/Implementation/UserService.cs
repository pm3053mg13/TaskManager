using TaskManagementApplication.Server.Core.Interfaces;
using TaskManagementApplication.Server.Domain;
using TaskManagementApplication.Server.Infrastructure;
using TaskManagementApplication.Server.Models;
using TaskManagementApplication.Server.Models.Request;

namespace TaskManagementApplication.Server.Core.Implementation
{
    [DefaultImplementation]
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService()
        {
        }
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Method to validate a user before hitting any api.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetUserTokenDetails(string token)
        {
            Guid validTokenGuid;

            if (!Guid.TryParse(token, out validTokenGuid))
                return null;

            var user = _userRepository.Fetch(x => x.UserToken == Guid.Parse(token) && x.UserTokenGenerationDate.AddMinutes(60) > DateTime.UtcNow).FirstOrDefault();

            if (user != null) { return user.UserToken.ToString(); }
            return null;
        }

        /// <summary>
        /// Method to create user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool CreateUser(CreateUserRequest request)
        {
            try
            {
                var user = new User()
                {
                    Username = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    RoleId = request.RoleId,
                    ManagerId = request.ManagerId,
                    UserToken = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = 1,
                    Password = "Pass123",
                    UserTokenGenerationDate = DateTime.UtcNow,
                    IsNew = true
                };

                _userRepository.Save(user);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Method to update user details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool UpdateUserDetail(UpdateUserRequest request)
        {
            try
            {
                var userDetails = _userRepository.Get(request.UserId);

                userDetails.FirstName = request.FirstName;
                userDetails.LastName = request.LastName;
                userDetails.Password = request.Password;
                userDetails.IsNew = false;

                _userRepository.Save(userDetails);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ManagerDetailModel> GetManagerDetailList()
        {
            var managerDetailList = new List<ManagerDetailModel>();
            var managers = _userRepository.Fetch(x => x.RoleId == 2).ToList();

            foreach (var manager in managers)
            {
                var managerDetail = new ManagerDetailModel
                {
                    Id = manager.Id,
                    Name = manager.GetFullName(manager.FirstName, manager.LastName)
                };

                managerDetailList.Add(managerDetail);
            }

            return managerDetailList;
        }
    }
}
