using BusinessLogics.Repositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogics.RepositoryImpl
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly GroupProjectContext _context;
        public CategoryRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }
        public void Create(Category category)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
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
                var category = _context.Categories.Find(id);
                if (category == null)
                    throw new KeyNotFoundException("Category not found.");

                _context.Categories.Remove(category);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(Guid id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id)
                   ?? throw new KeyNotFoundException("Category not found.");
        }

        public void Update(Category category)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existingCategory = _context.Categories.Find(category.CategoryId);
                if (existingCategory == null)
                    throw new KeyNotFoundException("Category not found.");

                existingCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
