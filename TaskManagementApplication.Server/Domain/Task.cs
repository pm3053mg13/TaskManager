using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementApplication.Server.Core;

namespace TaskManagementApplication.Server.Domain
{
    /// <summary>
    /// domain to create task table in database
    /// </summary>
    public class Task : DomainBase
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; } = null!;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
