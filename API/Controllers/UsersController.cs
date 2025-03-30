using AutoMapper;
using BusinessLogics.Repositories;
using DataAccess.DTOs.Users;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("odata/Users")]
    public class UsersController : ODataController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetById(Guid id)
        {
            try
            {
                var user = _userRepository.GetById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/users
        [HttpPost]
        public ActionResult Create([FromBody] UserDTO model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid user data." });
            var user = _mapper.Map<User>(model);
            _userRepository.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] UserDTO model)
        {
            if (model == null || id != model.UserId)
                return BadRequest(new { message = "Invalid user data." });

            try
            {
                var existingUser = _userRepository.GetById(id);
                if (existingUser == null)
                    return NotFound(new { message = "User not found." });

                // Map updated fields onto the existing user entity
                _mapper.Map(model, existingUser);

                _userRepository.Update(existingUser);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _userRepository.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}
