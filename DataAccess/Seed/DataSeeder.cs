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

        public void SeedRoles()
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
        }
    }
}
