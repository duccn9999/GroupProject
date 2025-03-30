using DataAccess.Models;

namespace BusinessLogics.Repositories
{
    public interface IRoleRepository
    {
        public List<Role> GetAll();
        public Role GetById(Guid id);
        public void Create(Role role);
        public void Update(Role role);
        public void Delete(Guid id);
    }
}
