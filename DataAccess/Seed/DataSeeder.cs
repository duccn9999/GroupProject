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
                    RoleName = "Admin"
                });
                _context.SaveChanges();
            }
        }
    }
}
