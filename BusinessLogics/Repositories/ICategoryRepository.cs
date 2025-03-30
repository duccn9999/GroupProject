using DataAccess.Models;

namespace BusinessLogics.Repositories
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();
        public Category GetById(Guid id);
        public void Create(Category category);
        public void Update(Category category);
        public void Delete(Guid id);
    }
}
