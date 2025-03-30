using BusinessLogics.Repositories;
using DataAccess.DTOs.Users;
using DataAccess.Models;

namespace BusinessLogics.RepositoryImpl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly GroupProjectContext _context;

        public UserRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    throw new KeyNotFoundException("User not found.");

                _context.Users.Remove(user);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<UserDTO> GetAll()
        {
            return _context.Users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password
            }).ToList();
        }

        public User GetById(Guid id)
        {
            return _context.Users.Find(id) ?? throw new KeyNotFoundException("User not found.");
        }



        public void Update(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existingUser = _context.Users.Find(user.UserId);
                if (existingUser == null)
                    throw new KeyNotFoundException("User not found.");

                _context.Users.Update(user);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

}
