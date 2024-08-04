using TaskManagementApplication.Server.Core.Interfaces;
using TaskManagementApplication.Server.Database;
using TaskManagementApplication.Server.Domain;
using TaskManagementApplication.Server.Infrastructure;
using TaskManagementApplication.Server.Models;

namespace TaskManagementApplication.Server.Core.Implementation
{
    [DefaultImplementation]
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> _userRepository;

        public LoginService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDetails ValidateUser(string username, string password)
        {
            var userData = _userRepository.Fetch(x => x.Username == username && x.Password == password).FirstOrDefault();

            if (userData != null)
            {
                //updating user token
                userData.UserToken = Guid.NewGuid();
                userData.UserTokenGenerationDate = DateTime.UtcNow;
                userData.IsNew = false;

                _userRepository.Save(userData);

                var userDetails = new UserDetails
                {
                    Id = userData.Id,
                    EmailId = userData.Email,
                    RoleId = userData.RoleId,
                    UserToken = userData.UserToken.ToString()
                };

                userDetails.Name = userData.GetFullName(userData.FirstName, userData.LastName);
                return userDetails;
            }
            else return null;
        }
    }
}
