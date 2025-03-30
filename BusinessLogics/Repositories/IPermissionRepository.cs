using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogics.Repositories
{
    public interface IPermissionRepository
    {
        public List<Permission> GetAll();
        public Permission GetById(Guid id);
        public List<Permission> GetPermissonsOfRole(Guid roleId);
    }
}
