﻿using BusinessLogics.Repositories;
using DataAccess.DTOs.Users;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogics.RepositoryImpl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly GroupProjectContext _context;

        public UserRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }

        public bool CheckUserNameExist(string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            return user == null ;
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

        public List<User> GetAll()
        {
            return _context.Users.ToList();
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

        public User ValidateUser(string userName, string password)
        {
            var user = _context.Users
                .Include(u => u.UserRoles) // Include roles if needed
                .FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }
            return user;
        }
    }

}
