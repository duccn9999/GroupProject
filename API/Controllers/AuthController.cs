using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BusinessLogics.Repositories;
using BusinessLogics.Service;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly AuthService _authService;

        public AuthController(IUserRepository userRepository, IMapper mapper, AuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _userRepository.ValidateUser(loginRequest.UserName, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _authService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpGet("getUser")]
        public IActionResult GetUser()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _authService.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized("Invalid token");
            }

            return Ok(user);
        }

    }

    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

}