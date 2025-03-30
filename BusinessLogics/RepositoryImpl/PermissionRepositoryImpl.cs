using BusinessLogics.Repositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogics.RepositoryImpl
{
    public class PermissionRepositoryImpl : IPermissionRepository
    {
        private readonly GroupProjectContext _context;
        public PermissionRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }

        public List<Permission> GetAll()
        {
            return _context.Permissions.ToList();
        }

        public Permission GetById(Guid id)
        {
            return _context.Permissions.Find(id);
        }

        public List<Permission> GetPermissionsOfRole(Guid roleId)
        {
            var role = _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefault(r => r.RoleId == roleId);

            if (role == null)
                throw new Exception("Role not found.");

            return role.RolePermissions.Select(rp => rp.Permission).ToList();
        }

        public List<Permission> GetPermissonsOfRole(Guid roleId)
        {
            throw new NotImplementedException();
        }
    }
}
