using BusinessLogics.Repositories;
using DataAccess.DTOs.Permissions;
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

        public List<PermissionDTO> GetPermissionsOfRole(Guid roleId)
        {
            var role = _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefault(r => r.RoleId == roleId);

            if (role == null)
                throw new Exception("Role not found.");

            return role.RolePermissions
                .Select(rp => new PermissionDTO
                {
                    PermissionId = rp.Permission.Id,
                    PermissionName = rp.Permission.PermissionName,
                    Value = rp.Permission.Value
                })
                .ToList();
        }

    }
}
