using AutoMapper;
using BusinessLogics.Repositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace API.Controllers
{
    [Route("odata/Roles")]
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
        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
        {
            var roles = _roleRepository.GetAll();
            return Ok(roles);
        }

        // GET odata/Roles/{id}
        [HttpGet("{id}")]
        public ActionResult<Role> GetById(Guid id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null)
                return NotFound(new { message = "Role not found." });
            return Ok(role);
        }

        // POST odata/Roles
        [HttpPost]
        public ActionResult Create([FromBody] Role model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid role data." });

            _roleRepository.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = model.RoleId }, model);
        }

        // PUT odata/Roles/{id}
        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] Role model)
        {
            if (model == null || id != model.RoleId)
                return BadRequest(new { message = "Invalid role data." });

            var existingRole = _roleRepository.GetById(id);
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