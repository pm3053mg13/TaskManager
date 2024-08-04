
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagementApplication.Server.Core.Interfaces;

namespace TaskManagementApplication.Server.Infrastructure.Authorization
{
    public class UserAuthorizationHandler : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public UserAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            .OfType<AllowAnonymousAttribute>()
            .Any();

            if (allowAnonymous) { return; }

            if (!_httpContextAccessor.HttpContext!.Request.Headers["Authorization"].Any())
            {
                context.Result = new UnauthorizedObjectResult("401");
                return;
            }
            else
            {
                var authorizationValue = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"].FirstOrDefault();

                if (string.IsNullOrEmpty(authorizationValue))
                {
                    context.Result = new UnauthorizedObjectResult("401");
                    return;
                }
                else
                {
                    var token = authorizationValue.Split(" ").Last();
                    var userToken = _userService.GetUserTokenDetails(token);
                    if (string.IsNullOrEmpty(userToken))
                    {
                        context.Result = new UnauthorizedObjectResult("401");
                        return;
                    }
                    return;
                }
            }
        }
    }
}
