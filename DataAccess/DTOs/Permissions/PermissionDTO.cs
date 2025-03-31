using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Permissions
{
    public class PermissionDTO
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Value { get; set; }
    }
}
