using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementApplication.Server.Core;

namespace TaskManagementApplication.Server.Domain
{
    /// <summary>
    /// Domain to create status table
    /// </summary>
    public class Status : DomainBase
    {
        /// <summary>
        /// Gets or Sets status name
        /// </summary>
        public string Name { get; set; }

        [ForeignKey("Id")]
        public virtual List<Domain.Task> Tasks { get; set; } = new List<Task>();
    }
}
