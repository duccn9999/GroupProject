using BusinessLogics.Repositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogics.RepositoryImpl
{
    public class RolePermissionRepositoryImpl : IRolePermissonRepository
    {
        private readonly GroupProjectContext _context;
        public RolePermissionRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }

        public List<Guid> GetPermissonsOfRole(Guid roleId)
        {
            var permissons = _context.RolePermissions.Where(x => x.RoleId == roleId).Select(x => x.PermissionId).ToList();
            return permissons;
        }

        public void UpdateRolePermissionOfRole(Guid roleId, List<Guid> permissionIds)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var role = _context.Roles
                    .Include(r => r.RolePermissions)
                    .FirstOrDefault(r => r.RoleId == roleId);

                if (role == null)
                    throw new Exception("Role not found.");

                // Get current permission IDs
                var currentPermissionIds = role.RolePermissions.Select(rp => rp.PermissionId).ToList();

                // Find permissions to remove
                var permissionsToRemove = role.RolePermissions
                    .Where(rp => !permissionIds.Contains(rp.PermissionId))
                    .ToList();

                // Find permissions to add
                var permissionsToAdd = permissionIds
                    .Where(pid => !currentPermissionIds.Contains(pid))
                    .Select(pid => new RolePermission { RoleId = roleId, PermissionId = pid })
                    .ToList();

                // Remove old permissions
                _context.RolePermissions.RemoveRange(permissionsToRemove);

                // Add new permissions
                _context.RolePermissions.AddRange(permissionsToAdd);

                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}
