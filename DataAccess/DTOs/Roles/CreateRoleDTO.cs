using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTOs.Roles
{
    public class CreateRoleDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}
