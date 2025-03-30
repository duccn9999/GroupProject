using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("RolePermission")]
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }    // Navigation Property
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }  // Navigation Property
    }
}
