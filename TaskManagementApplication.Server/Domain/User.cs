using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TaskManagementApplication.Server.Core;

namespace TaskManagementApplication.Server.Domain
{
    /// <summary>
    /// Domain class for user table in database
    /// </summary>
    public class User : DomainBase
    {
        /// <summary>
        /// gets or sets username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// gets or set first name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// gets or sets last name of the user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// gets or sets email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// gets or sets password of the user
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// gets or sets created date
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// gets or sets created by
        /// </summary>
        public int CreatedBy { get; set; }

        [AllowNull]
        public int ManagerId { get; set; }

        public Guid UserToken { get; set; }
        public DateTime UserTokenGenerationDate { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        //one to many mapping
        [ForeignKey("Id")]
        public virtual ICollection<Task> Tasks { get; set; }

        public string GetFullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            return FirstName + " " + LastName;
        }
    }
}
