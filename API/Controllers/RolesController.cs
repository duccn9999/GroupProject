using AutoMapper;
using BusinessLogics.Repositories;
using DataAccess.DTOs.Roles;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Controllers
{
    [Route("odata/Roles")]
    [Authorize(Roles = "Admin")]
    public class RolesController : ODataController
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RolesController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        // GET: odata/Roles
        [EnableQuery]
        [HttpGet] // This should now be unique
        public ActionResult<IQueryable<Role>> GetAll()
        {
            var roles = _roleRepository.GetAll().AsQueryable();
            return Ok(roles);
        }


        // GET odata/Roles/{id}
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Role> GetById(Guid id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
                return NotFound(new { message = "Role not found." });
            return Ok(role);
        }

        // POST odata/Roles
        [HttpPost]
        public ActionResult Create([FromBody] CreateRoleDTO model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid role data." });
            var role = _mapper.Map<Role>(model);
            _roleRepository.Create(role);
            return Created(role);
        }

        // PUT odata/Roles/{id}
        [HttpPut]
        public ActionResult Update([FromBody] UpdateRoleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid role data." });

            var existingRole = _roleRepository.GetById(model.RoleId);
            if (existingRole == null)
                return NotFound(new { message = "Role not found." });

            _mapper.Map(model, existingRole);
            _roleRepository.Update(existingRole);

            return NoContent();
        }

        // DELETE odata/Roles/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var existingRole = _roleRepository.GetById(id);
            if (existingRole == null)
                return NotFound(new { message = "Role not found." });

            _roleRepository.Delete(id);
            return NoContent();
        }
    }
}