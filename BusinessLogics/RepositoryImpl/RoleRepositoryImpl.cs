using BusinessLogics.Repositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogics.RepositoryImpl
{
    public class RoleRepositoryImpl : IRoleRepository
    {
        private readonly GroupProjectContext _context;

        public RoleRepositoryImpl(GroupProjectContext context)
        {
            _context = context;
        }

        public void Create(Role role)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Roles.Add(role);
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
                var role = _context.Roles.Find(id);
                if (role == null)
                    throw new KeyNotFoundException("Role not found.");

                _context.Roles.Remove(role);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role GetById(Guid id)
        {
            return _context.Roles.Find(id) ?? throw new KeyNotFoundException("Role not found.");
        }

        public void Update(Role role)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existingRole = _context.Roles.Find(role.RoleId);
                if (existingRole == null)
                    throw new KeyNotFoundException("Role not found.");
                _context.Roles.Update(role);
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
