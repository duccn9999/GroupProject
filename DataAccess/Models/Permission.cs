using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        public Guid Id { get; set; }
        public string PermissionName { get; set; }
        public string Value { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

    }
}
