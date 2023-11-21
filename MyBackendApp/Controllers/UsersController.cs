using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.DTO;
using MyBackendApp.Repository;

namespace MyBackendApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;
        public UsersController(IUser user)
        {
            _user = user;
        }


        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(UserCreateDto userCreateDto)
        {
            try
            {
                await _user.Registration(userCreateDto);
                return Ok($"Registration User {userCreateDto.Username} success");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(UserCreateDto userCreateDto)
        {
            try
            {
                var user = await _user.Authenticate(userCreateDto);
                if (user == null)
                    return BadRequest($"Username or Password doesnt match !");
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(RoleCreateDTO roleCreateDTO)
        {
            try
            {
                await _user.AddRole(roleCreateDTO.rolename);
                return Ok($"Role {roleCreateDTO.rolename} created");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegisterUserToRole")]
        public async Task<IActionResult> RegisterUserToRole(string username, string rolename)
        {
            try
            {
                await _user.RegisterUserToRole(username, rolename);
                return Ok($"Register user {username} to role {rolename}");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}