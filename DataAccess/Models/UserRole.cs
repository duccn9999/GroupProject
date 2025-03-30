using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("UserRole")]
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }  // ✅ Navigation Property

        public Guid RoleId { get; set; }
        public Role Role { get; set; }  // ✅ Navigation Property
    }

}
