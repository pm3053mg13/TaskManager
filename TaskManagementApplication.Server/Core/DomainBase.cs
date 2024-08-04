using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApplication.Server.Core
{
    public class DomainBase
    {
        public int Id { get; set; }
        [NotMapped]
        public bool IsNew { get; set; }
    }
}
