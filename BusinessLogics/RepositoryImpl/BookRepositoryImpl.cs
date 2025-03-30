using BusinessLogics.Repositories;
using DataAccess.DTOs.Books;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogics.RepositoryImpl
{
    public class BookRepositoryImpl : IBookRepository
    {
        private readonly GroupProjectContext _context;
        public BookRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }
        public void Create(Book book)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Books.Add(book);
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
                var book = _context.Books.Find(id);
                if (book == null)
                    throw new KeyNotFoundException("Book not found.");

                _context.Books.Remove(book);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book Find(Guid id)
        {
            return _context.Books.Find(id)
                   ?? throw new KeyNotFoundException("Book not found.");
        }

        public void Update(Book book)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existingBook = _context.Books.Find(book.BookId);
                if (existingBook == null)
                    throw new KeyNotFoundException("Book not found.");
                _context.Books.Update(book);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public GetBookDTO GetById(Guid id)
        {
            var book = _context.Books.Include(book => book.Category).FirstOrDefault(book => book.BookId == id);
            return new GetBookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                Image = book.Image,
                Price = book.Price,
                Stock = book.Stock,
                CategoryName = book.Category.CategoryName
            };
        }
    }
}
