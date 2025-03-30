using DataAccess.Models;

namespace DataAccess.Seed
{
    public class DataSeeder
    {
        private readonly GroupProjectContext _context;
        public DataSeeder(GroupProjectContext context)
        {
            _context = context;
        }

        public void CreateSeedData()
        {
            if (!_context.Roles.Any())
            {
                _context.Roles.Add(new Role
                {
                    RoleId = new Guid("88888888-8888-8888-8888-888888888888"),
                    RoleName = "Admin"
                });
                _context.SaveChanges();
            }
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    UserId = new Guid("99999999-9999-9999-9999-999999999999"),
                    UserName = "Admin",
                    Password = "123"
                });
                _context.SaveChanges();
            }
            if (!_context.UserRoles.Any())
            {
                _context.UserRoles.Add(new UserRole
                {
                    UserId = new Guid("99999999-9999-9999-9999-999999999999"),
                    RoleId = new Guid("88888888-8888-8888-8888-888888888888")
                });
                _context.SaveChanges();
            }
            if (!_context.Permissions.Any())
            {
                _context.Permissions.AddRange(new List<Permission>()
                {
                    new Permission { PermissionName = "Can manage books", Value = PermissionClaims.CAN_MANAGE_BOOKS },
                    new Permission { PermissionName = "Can add books", Value = PermissionClaims.CAN_ADD_BOOKS },
                    new Permission { PermissionName = "Can update books", Value = PermissionClaims.CAN_UPDATE_BOOKS },
                    new Permission { PermissionName = "Can delete books", Value = PermissionClaims.CAN_DELETE_BOOKS },
                    new Permission { PermissionName = "Can view book details", Value = PermissionClaims.CAN_VIEW_BOOK_DETAIL },
                    new Permission { PermissionName = "Can manage categories", Value = PermissionClaims.CAN_MANAGE_CATEGORIES },
                    new Permission { PermissionName = "Can add categories", Value = PermissionClaims.CAN_ADD_CATEGORIES },
                    new Permission { PermissionName = "Can update categories", Value = PermissionClaims.CAN_UPDATE_CATEGORIES },
                    new Permission { PermissionName = "Can delete categories", Value = PermissionClaims.CAN_DELETE_CATEGORIES },
                });
                _context.SaveChanges();
            }
            if (!_context.RolePermissions.Any())
            {
                var permissions = _context.Permissions.ToList();
                var roleId = Guid.Parse("88888888-8888-8888-8888-888888888888");

                _context.RolePermissions.AddRange(permissions.Select(p => new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = p.Id,
                }));
                _context.SaveChanges();
            }

        }
    }
}
