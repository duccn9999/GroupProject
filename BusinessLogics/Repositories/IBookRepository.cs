using DataAccess.DTOs.Books;
using DataAccess.Models;

namespace BusinessLogics.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetAll();
        public GetBookDTO GetById(Guid id);
        public Book Find(Guid id);
        public void Create(Book book);
        public void Update(Book book);
        public void Delete(Guid id);
    }
}
