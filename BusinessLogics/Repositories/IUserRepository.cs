using DataAccess.DTOs.Users;
using DataAccess.Models;

namespace BusinessLogics.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetAll();
        public User GetById(Guid id);
        public void Create(User user);
        public void Update(User user);
        public void Delete(Guid id);
        User ValidateUser(string userName, string password);
    }
}
