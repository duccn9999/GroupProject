using DataAccess.Models;

namespace BusinessLogics.Repositories
{
    public interface IPermissionRepository
    {
        public List<Permission> GetAll();
        public Permission GetById(Guid id);
        public List<Permission> GetPermissionsOfRole(Guid roleId);
    }
}
