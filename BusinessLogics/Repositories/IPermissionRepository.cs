using DataAccess.Models;

using DataAccess.DTOs.Permissions;
namespace BusinessLogics.Repositories
{
    public interface IPermissionRepository
    {
        public List<Permission> GetAll();
        public Permission GetById(Guid id);
        public List<PermissionDTO> GetPermissionsOfRole(Guid roleId);
    }
}
