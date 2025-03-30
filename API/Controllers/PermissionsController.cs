using AutoMapper;
using BusinessLogics.Repositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionsController(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet]
        public ActionResult<IQueryable<Permission>> GetAll()
        {
            var permissions = _permissionRepository.GetAll().AsQueryable();
            return Ok(permissions);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Role> GetById(Guid id)
        {
            var permission = _permissionRepository.GetById(id);
            if (permission == null)
                return NotFound(new { message = "Role not found." });
            return Ok(permission);
        }

        [HttpGet]
        [Route("GetPermissionOfRole/{roleId}")]
        public ActionResult<Role> GetPermissionOfRole(Guid roleId)
        {
            var permissions = _permissionRepository.GetPermissionsOfRole(roleId);
            if (permissions == null)
                return NotFound(new { message = "Not found." });
            return Ok(permissions);
        }
    }
}
