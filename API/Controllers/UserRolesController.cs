using AutoMapper;
using BusinessLogics.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserRolesController : ControllerBase
    {
        public IUserRoleRepository _userRolesRepository;
        public IMapper _mapper;
        public UserRolesController(IUserRoleRepository userRolesRepository, IMapper mapper)
        {
            _userRolesRepository = userRolesRepository;
            _mapper = mapper;
        }
        // PUT api/<UserRolesController>/5
        [HttpPut("UpdateRolesOfUser/{userId}")]
        public IActionResult Put(Guid userId, [FromBody] List<Guid> roleIds)
        {
            _userRolesRepository.UpdateRolesOfUser(userId, roleIds);
            return NoContent();
        }
    }
}
