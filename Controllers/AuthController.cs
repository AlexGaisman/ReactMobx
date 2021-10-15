using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Tracker.DTO;
using Tracker.Helpers;
using Tracker.Interfaces;
using Tracker.Models;
using Newtonsoft.Json;

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
        [HttpPost("signup")]
        public IActionResult Signup(UserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Id = userDto.Id,
                Password = BCrypt.Net.BCrypt.HashPassword(  userDto.Password),
                Role = userDto.Role
            };

           if( _userRepository.GetByEmail(user.Email) != null)
                return BadRequest(new { message= "Email already exists" });

            try
            {
                _userRepository.Create(user);
            }catch(Exception)
            {
                return BadRequest(new { message= "There was a problem creating your account" });
            }

            var jwt = _jwtService.Generate(user.Id);

            TimeSpan t = DateTime.UtcNow.AddHours(1) - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            user.Password = null;
            return Ok(new {
                message= "User created!",
                token = jwt,
                userInfo = user,
                expiresAt = secondsSinceEpoch
            });
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto userDto)
        {
            var user = _userRepository.GetByEmail(userDto.Email);

            if (user == null) return BadRequest(new { message = "Invalid Credentials" });

            if(!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
                return BadRequest(new { message = "Invalid Credentials" });

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

            TimeSpan t = DateTime.UtcNow.AddHours(1) - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            user.Password = null;
            return Ok(new { message= "Authentication successful!",
                            
                            userInfo =  user,
                expiresAt = secondsSinceEpoch,
                token = jwt
            });
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

        [HttpPatch("/api/user-role")]
        public IActionResult UserRole([FromBody] UserDto userDto)
        {
            var allowedRoles = new string[]{ "user", "admin" };

            if (!allowedRoles.Contains(userDto.Role))
                return Ok(new { message="Role not allowed"});

            try
            {
                _userRepository.UpdateRole(userDto.Id, userDto.Role);
                return Ok(new { message = "User role updated. You must log in again for the changes to take effect" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { error=ex.Message});
            }

        }

        [HttpGet("/api/dashboard-data")]
        public IActionResult DashboardData()
        {
            return Ok(new
            {
                salesVolume= 73977,
                newCustomers= 89,
                refunds= 2254,
                graphData= new[] {
                    new { date= "Jan 2019", amount= 1902 },
                    new { date= "Feb 2019", amount= 893 },
                    new { date= "Mar 2019", amount= 1293 },
                    new { date= "Apr 2019", amount= 723 },
                    new { date= "May 2019", amount= 2341 },
                    new { date= "Jun 2019", amount= 2113 },
                    new { date= "Jul 2019", amount= 236 },
                    new { date= "Aug 2019", amount= 578 },
                    new { date= "Sep 2019", amount= 912 },
                    new { date= "Oct 2019", amount= 2934 },
                    new { date= "Nov 2019", amount= 345 },
                    new { date= "Dec 2019", amount= 782 }
                }

            });
        }
    }
}
