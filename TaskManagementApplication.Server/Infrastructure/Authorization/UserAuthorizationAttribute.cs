
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagementApplication.Server.Infrastructure.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class UserAuthorizationAttribute : Attribute
    {
        public UserAuthorizationAttribute()
        {
            ImplementationType = typeof(UserAuthorizationAttribute);
        }
        public Type ImplementationType { get; }

    }
}
