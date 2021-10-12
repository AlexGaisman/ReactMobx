using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracker.DTO;
using Tracker.Helpers;
using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository userRepository,
            JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Id = userDto.Id,
                Password = BCrypt.Net.BCrypt.HashPassword(  userDto.Password)
            };

            _userRepository.Create(user);
            return Created("Success", user);
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto userDto)
        {
            var user = _userRepository.GetByEmail(userDto.Email);

            if (user == null) return BadRequest(new { message = "Invalid Credentials" });

            if(!BCrypt.Net.BCrypt.Verify(user.Password, userDto.Password))
                return BadRequest(new { message = "Invalid Credentials" });

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

            return Ok(new { message="success"});
        }
        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = Int32.Parse(token.Issuer);

                var user = _userRepository.GetById(userId);

                return Ok(user);
            }
            catch(Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "success" });
        }
    }
}
