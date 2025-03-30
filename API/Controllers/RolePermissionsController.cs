using AutoMapper;
using BusinessLogics.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("odata/RolePermissions")]
    public class RolePermissionsController : ODataController
    {
        private readonly IRolePermissonRepository _rolePermissonRepository;
        private readonly IMapper _mapper;
        public RolePermissionsController(IRolePermissonRepository rolePermissonRepository, IMapper mapper)
        {
            _rolePermissonRepository = rolePermissonRepository;
            _mapper = mapper;
        }

        // PUT api/<RolePermissionsController>/5
        [HttpPut("UpdateRolePermissionOfRole/{roleId}")]
        public IActionResult Put(Guid roleId, [FromBody] List<Guid> permissionIds)
        {
            _rolePermissonRepository.UpdateRolePermissionOfRole(roleId, permissionIds);
            return NoContent();
        }
    }
}
