using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementApplication.Server.Core;

namespace TaskManagementApplication.Server.Domain
{
    /// <summary>
    /// Domain file to create roles
    /// </summary>
    public class Role : DomainBase
    {
        /// <summary>
        /// gets or sets role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// One-Many relation
        /// </summary>
        public ICollection<User> User { get; set; }
    }
}
