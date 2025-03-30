using BusinessLogics.Repositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogics.RepositoryImpl
{
    public class UserRoleRepositoryImpl : IUserRoleRepository
    {
        private readonly GroupProjectContext _context;

        public UserRoleRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }

        public List<Guid> GetRolesOfUser(Guid userId)
        {
            var userRoles = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            return userRoles;
        }

        public void UpdateRolesOfUser(Guid userId, List<Guid> roleIds)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = _context.Users
                    .Include(u => u.UserRoles) // Assuming UserRoles is the navigation property
                    .FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                    throw new Exception("User not found.");

                // Get current role IDs
                var currentRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();

                // Find roles to remove
                var rolesToRemove = user.UserRoles
                    .Where(ur => !roleIds.Contains(ur.RoleId))
                    .ToList();

                // Find roles to add
                var rolesToAdd = roleIds
                    .Where(rid => !currentRoleIds.Contains(rid))
                    .Select(rid => new UserRole { UserId = userId, RoleId = rid })
                    .ToList();

                // Remove old roles
                _context.UserRoles.RemoveRange(rolesToRemove);

                // Add new roles
                _context.UserRoles.AddRange(rolesToAdd);

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
